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
        if (PasswordEntry.IsPassword)
        {
            ShowPass.ImageSource = "closed_eye.png";
        }
        else
        {
            ShowPass.ImageSource = "open_eye.png";
        }
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
                        await DisplayAlert("�����", "����������� ���������!", "OK");
                        await Navigation.PopAsync(); 
                    }
                    else
                    {
                        await DisplayAlert("������", "Email ��� ���������������!", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("������", "�� ������ ����������� � ���������", "OK");
                }
            }
            else
            {
                await DisplayAlert("������", "������ �� ���������", "OK");
            }
        }
        else
        {
            await DisplayAlert("������", "��������� ��� ����", "OK");
        }
    }


    private async void logBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginForm());
    }
}