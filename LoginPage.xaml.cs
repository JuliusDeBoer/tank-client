using tank_client.Models;

namespace tank_client;

public partial class LoginPage : ContentPage
{
	public static string Auth = string.Empty;
	public static int MyTankId = -1;

	public LoginPage()
	{
		InitializeComponent();
	}

// This is dumb
#nullable enable

	private void SignUp(Object sender, EventArgs e)
    {
		Server.Invoke("CreateAccount", SignUpNameEntry.Text, SignUpEmailEntry.Text, SignUpPasswordEntry.Text);
		string? jwt = Server.Invoke<string>("Login", SignUpEmailEntry.Text, SignUpPasswordEntry.Text);

		if (jwt == null)
		{
			ErrorLabel.Text = "Error signing up";
			return;
		}

		Auth = jwt;
        MyTankId = Server.Invoke<int>("GetMyTankId", Auth);

        Navigation.PushAsync(new MainPage());
    }
	private void Login(Object sender, EventArgs e)
	{
		string? jwt = Server.Invoke<string>("Login", LoginEmailEntry.Text, LoginPasswordEntry.Text);

		if (jwt == null)
		{
			ErrorLabel.Text = "Error loggin in";
			return;
		}

		Auth = jwt;
		MyTankId = Server.Invoke<int>("GetMyTankId", Auth);

		Navigation.PushAsync(new MainPage());
	}
#nullable disable
}