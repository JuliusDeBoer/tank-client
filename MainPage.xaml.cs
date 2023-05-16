namespace tank_client;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    void OnPointerMoved(object sender, PointerEventArgs e)
    {
		Point? pos = e.GetPosition(this);
		if(!pos.HasValue) { return; }

		Title = $"X: {pos.Value.X} Y: {pos.Value.Y}";
    }
}