using tank_client.Models;

namespace tank_client;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private void SignUp(Object sender, EventArgs e)
    {
		Server.Invoke("CreateAccount", NameEntry.Text, EmailEntry.Text, PasswordEntry.Text);
		string jwt = Server.Invoke<string>("Login", EmailEntry.Text, PasswordEntry.Text);
    }
}