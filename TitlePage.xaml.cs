namespace tank_client;

public partial class TitlePage : ContentPage
{
	public TitlePage()
	{
		InitializeComponent();
	}

    private void Start(Object sender, EventArgs e)
    {
        StartButton.Text = "Loading...";
		Navigation.PushAsync(new MainPage());
    }
}