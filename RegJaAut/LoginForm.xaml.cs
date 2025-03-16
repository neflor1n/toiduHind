using toiduHind.DatabaseModels;

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
        ShowPass.ImageSource = PasswordEntry.IsPassword ? "closed_eye.png" : "open_eye.png";
    }

    private async void LoginBtn_Clicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Ошибка", "Заполните все поля!", "OK");
            return;
        }

        var user = await _database.GetUserByEmail(email);
        if (user != null && user.Password == password) 
        {
            await DisplayAlert("Успех", "Вход выполнен!", "OK");
            await Navigation.PushAsync(new AvaLeht());
        }
        else
        {
            await DisplayAlert("Ошибка", "Неверный email или пароль", "OK");
        }
    }
}
