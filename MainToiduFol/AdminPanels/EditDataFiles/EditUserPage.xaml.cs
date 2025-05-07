using System;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;
using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels
{
    public partial class EditUserPage : ContentPage
    {
        private readonly Database _database;
        private User _user;
        private readonly int _userId;
        private string _originalEmail;

        public EditUserPage(int userId)
        {
            InitializeComponent();
            _database = new Database();
            _userId = userId;
            LoadUserDataAsync();
        }

        private async void LoadUserDataAsync()
        {
            try
            {
                _user = await _database.GetUserByIdAsync(_userId);
                if (_user != null)
                {
                    // Populate fields with user data
                    NameEntry.Text = _user.Name;
                    EmailEntry.Text = _user.Email;
                    _originalEmail = _user.Email;

                    // Set role in picker
                    int roleIndex = _user.Role == "Admin" ? 1 : 0;
                    RolePicker.SelectedIndex = roleIndex;
                }
                else
                {
                    await DisplayAlert("Viga", "Kasutajat ei leitud", "OK");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Andmete laadimise viga: {ex.Message}", "OK");
                await Navigation.PopAsync();
            }
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

                // Validate email format
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(EmailEntry.Text, emailPattern))
                {
                    await DisplayAlert("Viga", "Palun sisesta korrektne email aadress", "OK");
                    return;
                }

                // Check if email changed and if it already exists
                if (EmailEntry.Text != _originalEmail)
                {
                    var existingUser = await _database.GetUserByEmail(EmailEntry.Text);
                    if (existingUser != null && existingUser.Id != _userId)
                    {
                        await DisplayAlert("Viga", "Selle e-posti aadressiga kasutaja on juba olemas", "OK");
                        return;
                    }
                }

                // Validate passwords if provided
                if (!string.IsNullOrWhiteSpace(PasswordEntry.Text))
                {
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
                }

                // Update user data
                _user.Name = NameEntry.Text;
                _user.Email = EmailEntry.Text;
                _user.Role = RolePicker.SelectedItem.ToString();

                // Update password only if provided
                if (!string.IsNullOrWhiteSpace(PasswordEntry.Text))
                {
                    _user.Password = PasswordEntry.Text;
                }

                // Save to database
                await _database.UpdateUser(_user);
                await DisplayAlert("Õnnestus", "Kasutaja andmed on uuendatud", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Kasutaja uuendamine ebaõnnestus: {ex.Message}", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}