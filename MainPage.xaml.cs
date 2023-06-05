using tank_client.Models;

namespace tank_client;

public partial class MainPage : ContentPage
{
    public static readonly int GRID_WIDTH = 16;
    public static readonly int GRID_HEIGHT = 12;
    public static readonly int CELL_SIZE = 100;

    private bool pressed = false;
    private bool shouldUpdateAnchor = true;
    private double anchorX = 0;
    private double anchorY = 0;

    private int selectedTank = -1;

    private readonly TankCollection collection;

    public MainPage()
    {
        InitializeComponent();

        for (int i = 0; i < GRID_HEIGHT; i++)
        {
            ChessMaster.RowDefinitions.Add(new RowDefinition { Height = CELL_SIZE });
            Overlay.RowDefinitions.Add(new RowDefinition { Height = CELL_SIZE });
        }

        for (int i = 0; i < GRID_WIDTH; i++)
        {
            ChessMaster.ColumnDefinitions.Add(new ColumnDefinition { Width = CELL_SIZE });
            Overlay.ColumnDefinitions.Add(new ColumnDefinition { Width = CELL_SIZE });
        }

        collection = Server.Invoke<TankCollection>("GetTanks");
        Random rand = new();

        foreach (Tank tank in collection.Tanks)
        {
            ImageButton element = new()
            {
                WidthRequest = CELL_SIZE,
                HeightRequest = CELL_SIZE,
                Source = "tank_red.png",
                Aspect = Aspect.Fill,
                BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgba(0, 0, 0, 0),
                ScaleX = rand.NextSingle() < 0.5f ? 1 : -1
            };

            element.Clicked += ImageButton_Clicked;

            ChessMaster.Add(element);
            ChessMaster.SetColumn(element, tank.Position.X);
            ChessMaster.SetRow(element, tank.Position.Y);

            Log($"ID: {tank.Id} X: {tank.Position.X} Y: {tank.Position.Y}");
        }
    }

    void MoveBattleground(object sender, PointerEventArgs e)
    {
        if (!pressed) { return; }


        Point? pos = e.GetPosition(this);
        if (!pos.HasValue) { return; }


        if (shouldUpdateAnchor)
        {
            anchorX = pos.Value.X - battleground.TranslationX;
            anchorY = pos.Value.Y - battleground.TranslationY;
            shouldUpdateAnchor = false;
        }

        Title = $"X: {pos.Value.X} Y: {pos.Value.Y}";

        battleground.TranslationX = pos.Value.X - anchorX;
        battleground.TranslationY = pos.Value.Y - anchorY;
    }

    private void BattlegroundPressed(object sender, EventArgs e)
    {
        shouldUpdateAnchor = true;
        pressed = true;

        CleanOverlay();

        selectedTank = -1;
        UserName.Text = String.Empty;
        Health.Text = String.Empty;
        Shoot.IsVisible = false;
    }

    private void BattlegroundReleased(object sender, EventArgs e)
    {
        Log($"Battlegrounds new position is X: {battleground.TranslationX} Y: {battleground.TranslationY}");
        pressed = false;
    }

    private class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    private void CleanOverlay()
    {
        // This is bad and dosnt solve the issue. But im not inclined to care
        for(int  j = 0; j <= 32; j++)
        {
            for(int i = 0; i < Overlay.Count; i++)
            {
                if (Overlay[i] is ImageButton)
                {
                    Overlay.RemoveAt(i);
                }
            }
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button)
        {
            int row = ChessMaster.GetRow(button);
            int col = ChessMaster.GetColumn(button);
            selectedTank = collection.GetTankByPos(col, row);

            Log($"Tank with id {selectedTank} was on Row: {row} and Col: {col}");

            Tank tank = collection.GetById(selectedTank);

            CleanOverlay();

            int gridSize = (tank.Level * 2) + 1;
            int center = tank.Level;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if(i == center && j == center) { continue; }

                    ImageButton element = new()
                    {
                        WidthRequest = CELL_SIZE,
                        HeightRequest = CELL_SIZE,
                        Source = "circle_green.png",
                        Aspect = Aspect.Fill,
                        BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgba(0, 0, 0, 0),
                        Opacity = 0.5f
                    };

                    try
                    {
                        Overlay.SetColumn(element, tank.Position.X + (i - center));
                        Overlay.SetRow(element, tank.Position.Y + (j - center));
                    }
                    catch (System.ArgumentException ex)
                    {
                        // Dont draw circles that are not inside the map
                    }

                    Overlay.Add(element);
                }
            }

            UserName.Text = tank.UserName;
            Health.Text = $"{tank.Health.ToString()} HITPOINTS";
            Shoot.IsVisible = true;
        }
    }

    public void Log(string msg)
    {
        // Make sure code gets run on the UI's dispatcher thread
        Dispatcher.Dispatch(() =>
        {
            logView.Text = msg + "\n" + logView.Text;
        });
    }
}