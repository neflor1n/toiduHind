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
		var anonUser = new toiduHind.DatabaseModels.User()
		{
			Name = "Guest",
			Email = "guest@toiduhind.ee",
			Password = ""
		};
		Button btn = sender as Button;
		await Navigation.PushAsync(new MainToiduFol.HomePage.HomePage(anonUser));
	}
	
	private async void RegBtn_Clicked(object sender, EventArgs e)
	{
	Button btn = sender as Button;
	await Navigation.PushAsync(new RegJaAut.RegistreerimineForm());
	}
	
	private async void logBtn_Clicked(object sender, EventArgs e)
	{
	Button btn = sender as Button;
	await Navigation.PushAsync(new RegJaAut.LoginForm()); 
	}
}
