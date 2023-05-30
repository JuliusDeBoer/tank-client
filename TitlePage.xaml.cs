using tank_client.Models;

namespace tank_client;

public partial class TitlePage : ContentPage
{
	public TitlePage()
	{
		InitializeComponent();

        new Thread(Server.Connect).Start();
    }

    private void Start(Object sender, EventArgs e)
    {
		Navigation.PushAsync(new LoginPage());
    }

    private void Credits(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreditsPage());
    }
}