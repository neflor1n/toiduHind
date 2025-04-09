namespace toiduHind.MainToiduFol.SettingsPage;

using System.Diagnostics;
using toiduHind.DatabaseModels;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Alerts;
using MailKit.Net.Smtp;
using MimeKit;

public partial class SettingsPage : ContentPage
{
    private readonly User _user;

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


    private void OnTogglePasswordVisibility(object sender, EventArgs e)
    {
        CurrentPasswordEntry.IsPassword = !CurrentPasswordEntry.IsPassword;
        //ShowPass.ImageSource = CurrentPasswordEntry.IsPassword ? "closed_eye.png" : "open_eye.png";
    }

}
