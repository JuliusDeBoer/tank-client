namespace tank_client;

public partial class TitlePage : ContentPage
{
	public TitlePage()
	{
		InitializeComponent();
    }

    private void Start(Object sender, EventArgs e)
    {
		Navigation.PushAsync(new MainPage());
    }
}