namespace toiduHind.RegJaAut;

public partial class LoginForm : ContentPage
{
	public LoginForm()
	{
		InitializeComponent();
	}
    private void OnTogglePasswordVisibility(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        if (PasswordEntry.IsPassword)
        {
            ShowPass.ImageSource = "closed_eye.png";  
        } else
        {
            ShowPass.ImageSource = "open_eye.png";
        }
        
    }

}