﻿using toiduHind.DatabaseModels;
using toiduHind.MainToiduFol.HomePage;

namespace toiduHind.RegJaAut;

public partial class LoginForm : ContentPage
{
    private Database _database = new Database();

    public LoginForm()
    {
        InitializeComponent();
    }

    private void OnTogglePasswordVisibility(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        ShowPass.ImageSource = PasswordEntry.IsPassword ? "closed_eye_white.png" : "opened_eye_white.png";
    }

    private async void LoginBtn_Clicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Viga", "Palun täitke kõik väljad!", "OK");
            return;
        }

        var user = await _database.GetUserByEmail(email);
        if (user != null && user.Password == password)
        {
            await DisplayAlert("Õnnestus", "Sisselogimine õnnestus!", "OK");
            App.CurrentUser = user;
            await Navigation.PushAsync(new MainToiduFol.HomePage.HomePage(user));

        }
        else
        {
            await DisplayAlert("Viga", "Vale e-posti aadress või parool", "OK");
        }
    }
}
