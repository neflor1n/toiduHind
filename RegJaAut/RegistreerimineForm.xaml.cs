using SQLite;
using System.Diagnostics;
using toiduHind.DatabaseModels;

namespace toiduHind.RegJaAut;
public partial class RegistreerimineForm : ContentPage
{
    private Database _database = new Database();

    public RegistreerimineForm()
    {
        InitializeComponent();
    }

    public void OnTogglePasswordVisibility(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        ShowPass.ImageSource = PasswordEntry.IsPassword ? "closed_eye.png" : "open_eye.png";
    }

    private async void regBtn_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(NameEntry.Text) &&
            !string.IsNullOrEmpty(PasswordEntry.Text) &&
            !string.IsNullOrEmpty(ConfPasswordEntry.Text) &&
            !string.IsNullOrEmpty(EmailEntry.Text))
        {
            if (PasswordEntry.Text == ConfPasswordEntry.Text)
            {
                if (kasutajatingimust.IsChecked)
                {
                    User newUser = new User
                    {
                        Name = NameEntry.Text,
                        Email = EmailEntry.Text,
                        Password = PasswordEntry.Text
                    };

                    bool success = await _database.RegisterUser(newUser);
                    if (success)
                    {
                        await DisplayAlert("Õnnestus", "Kasutaja registreerimine õnnestus!", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Viga", "Selle e-posti aadressiga on juba konto olemas!", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Viga", "Palun nõustu kasutustingimustega.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Viga", "Paroolid ei kattu!", "OK");
            }
        }
        else
        {
            await DisplayAlert("Viga", "Palun täida kõik väljad!", "OK");
        }
    }

    private async void logBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginForm());
    }
}
