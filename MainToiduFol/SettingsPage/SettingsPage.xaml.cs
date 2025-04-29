namespace toiduHind.MainToiduFol.SettingsPage;

using System.Diagnostics;
using toiduHind.DatabaseModels;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Alerts;
using MailKit.Net.Smtp;
using MimeKit;
using toiduHind.MainToiduFol.AdminPanels;

public partial class SettingsPage : ContentPage
{
    private readonly User _user;
    private bool IsGuest => _user.Email == "guest@toiduhind.ee";
    private bool IsAdmin => _user.Name == "neflor1n";

    public SettingsPage(User user)
    {
        InitializeComponent();
        _user = user;
    }

    private string _generatedEmailCode = "";

    // Подтверждение изменения Email адреса
    private async void OnConfirmEmailClicked(object sender, EventArgs e)
    {
        string newEmail = NewEmailEntry.Text;

        if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
        {
            await ShowToast("Sisesta korrektne e-posti aadress");
            return;
        }

        var existingUser = await App.Database.GetUserByEmail(newEmail);
        if (existingUser != null && existingUser.Id != _user.Id)
        {
            await ShowToast("See e-posti aadress on juba kasutusel.");
            return;
        }

        _generatedEmailCode = new Random().Next(100000, 999999).ToString();

        bool sent = await SendEmailCodeAsync(newEmail, _generatedEmailCode);

        if (!sent)
        {
            await ShowToast("Koodi saatmine ebaõnnestus");
            return;
        }

        string result = await DisplayPromptAsync("Kinnitus", $"Sisesta kood, mis saadeti: {newEmail}", "OK", "Katkesta");

        if (result == _generatedEmailCode)
        {
            _user.Email = newEmail;
            await App.Database.UpdateUser(_user);
            await ShowToast("E-post on uuendatud!");
        }
        else
        {
            await ShowToast("Vale kood!");
        }
    }


    // Метод меняющий пароль на новый 
    private async void OnChangePasswordClicked(object sender, EventArgs e)
    {
        string current = CurrentPasswordEntry.Text;
        string pass1 = NewPasswordEntry.Text;
        string pass2 = ConfirmPasswordEntry.Text;

        if (_user.Password != current)
        {
            await ShowToast("Vale praegune parool!");
            return;
        }

        if (string.IsNullOrWhiteSpace(pass1) || pass1.Length < 6)
        {
            await ShowToast("Uus parool peab olema vähemalt 6 tähemärki");
            return;
        }

        if (pass1 != pass2)
        {
            await ShowToast("Uued paroolid ei kattu");
            return;
        }

        _user.Password = pass1;
        await App.Database.UpdateUser(_user);
        await ShowToast("Parool on muudetud!");
    }

    

    private async Task ShowToast(string message)
    {
        var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short);
        await toast.Show();
    }


    // Отправка кода на почту пользотеля с кодом
    private async Task<bool> SendEmailCodeAsync(string email, string code)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ToiduHind", "lanor1n23@gmail.com")); 
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Kinnituskood e-posti vahetamiseks";

            message.Body = new TextPart("plain")
            {
                Text = $"Tere!\n\nTeie kinnituskood on: {code}\n\nKui see ei olnud teie tegevus, võite selle kirja eirata."
            };

            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true; 
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("lanor1n23@gmail.com", "eovt qzjd cnsd lcsr"); 
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[SMTP ERROR] {ex.Message}");
            return false;
        }
    }

    // Метод для показа/скрытия пароля
    private void OnToggleNewPasswordVisibilityClicked(object sender, EventArgs e)
    {
        NewPasswordEntry.IsPassword = !NewPasswordEntry.IsPassword;
        ToggleNewPasswordVisibility.Source = NewPasswordEntry.IsPassword ? "closed_eye_white.png" : "opened_eye_white.png";
    }

    // Метод выхода из аккаунта 
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Logi välja", "Oled kindel, et soovid välja logida?", "Jah", "Ei");
        if (confirm)
        {
            
            await Navigation.PopToRootAsync(); 
        }
    }

    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        EmailExpander.IsVisible = !IsGuest;
        PasswordExpander.IsVisible = !IsGuest;
        LoginPromptButton.IsVisible = IsGuest;
        LogoutButton.IsVisible = !IsGuest;

        AdminFeaturesLayout.IsVisible = IsAdmin;
    }


    private async void OnLoginRedirectClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegJaAut.LoginForm());
    }


    private async void OnAddStoreClicked(object sender, EventArgs e)
    {
        //await DisplayAlert("Admin", "Funktsioon 'Lisa uus pood' ei ole veel implementeeritud.", "OK");

        await Navigation.PushAsync(new AdminTabbedPage());
    }

    private async void OnImportCsvClicked(object sender, EventArgs e)
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("stores.csv");
            using var reader = new StreamReader(stream);
            var header = await reader.ReadLineAsync(); 

            int count = 0;
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var parts = line.Split(',');

                if (parts.Length < 3) continue;

                var store = new Store
                {
                    Name = parts[0],
                    LogoFileName = parts[1],
                    Location = parts[2],
                    CreatedAt = DateTime.UtcNow
                };

                await App.Database.AddStoreAsync(store); 
                count++;
            }

            await DisplayAlert("Õnnestus", $"{count} poodi imporditi edukalt!", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", $"Impordi ebaõnnestus: {ex.Message}", "OK");
        }
    }


}
