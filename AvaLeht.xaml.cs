using System.Threading.Tasks;

namespace toiduHind;

public partial class AvaLeht : ContentPage
{
	public AvaLeht()
	{
		InitializeComponent();
        //Bogdan's commit
	}
    private async void JatkakeIlmaReg_Clicked(object sender, EventArgs e)
    {

		//Button btn = sender as Button;
		//await Navigation.PushAsync(new );
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