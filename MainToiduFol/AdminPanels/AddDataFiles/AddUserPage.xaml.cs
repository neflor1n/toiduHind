using System;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;
using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels
{
    public partial class AddUserPage : ContentPage
    {
        private readonly Database _database;

        public AddUserPage()
        {
            InitializeComponent();
            _database = new Database();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(NameEntry.Text))
                {
                    await DisplayAlert("Viga", "Palun sisesta nimi", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(EmailEntry.Text))
                {
                    await DisplayAlert("Viga", "Palun sisesta email", "OK");
                    return;
                }

                // Validate email format using regex
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(EmailEntry.Text, emailPattern))
                {
                    await DisplayAlert("Viga", "Palun sisesta korrektne email aadress", "OK");
                    return;
                }

                // Check if user with this email already exists
                var existingUser = await _database.GetUserByEmail(EmailEntry.Text);
                if (existingUser != null)
                {
                    await DisplayAlert("Viga", "Selle e-posti aadressiga kasutaja on juba olemas", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
                {
                    await DisplayAlert("Viga", "Palun sisesta parool", "OK");
                    return;
                }

                // Password strength validation (optional)
                if (PasswordEntry.Text.Length < 6)
                {
                    await DisplayAlert("Viga", "Parool peab sisaldama vähemalt 6 tähemärki", "OK");
                    return;
                }

                if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
                {
                    await DisplayAlert("Viga", "Paroolid ei kattu", "OK");
                    return;
                }

                // Create new user
                var user = new User
                {
                    Name = NameEntry.Text,
                    Email = EmailEntry.Text,
                    Password = PasswordEntry.Text,
                    Role = RolePicker.SelectedItem.ToString()
                };

                // Add user to database
                try
                {
                    await _database.AddUserAsync(user);
                    await DisplayAlert("Õnnestus", "Kasutaja on edukalt lisatud", "OK");
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Viga", $"Kasutaja lisamine ebaõnnestus: {ex.Message}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Tekkis viga: {ex.Message}", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
