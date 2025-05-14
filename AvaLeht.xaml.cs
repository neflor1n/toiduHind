using System.Threading.Tasks;

namespace toiduHind;

public partial class AvaLeht : ContentPage
{
	public AvaLeht()
	{
		InitializeComponent();
	}
	private async void JatkakeIlmaReg_Clicked(object sender, EventArgs e)
	{
        ShowLoading(true);
        var anonUser = new toiduHind.DatabaseModels.User
        {
            Name = "Guest",
            Email = "guest@toiduhind.ee",
            Password = ""
        };
        await Task.Delay(300);
        await Navigation.PushAsync(new MainToiduFol.HomePage.HomePage(anonUser));
        ShowLoading(false);
    }
	
	private async void RegBtn_Clicked(object sender, EventArgs e)
	{
		Button btn = sender as Button;
        ShowLoading(true);
        await Task.Delay(300);
        await Navigation.PushAsync(new RegJaAut.RegistreerimineForm());
        ShowLoading(false);
    }
	
	private async void logBtn_Clicked(object sender, EventArgs e)
	{
		Button btn = sender as Button;
        ShowLoading(true);
        await Task.Delay(300);
		await Navigation.PushAsync(new RegJaAut.LoginForm());
		ShowLoading(false);
	}

    private void ShowLoading(bool show)
    {
        LoadingIndicator.IsVisible = show;
        LoadingIndicator.IsRunning = show;
    }

}
