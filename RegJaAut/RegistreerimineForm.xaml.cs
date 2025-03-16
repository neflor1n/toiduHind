namespace toiduHind.RegJaAut;

public partial class RegistreerimineForm : ContentPage
{
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
}