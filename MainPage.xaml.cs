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

    private TankCollection collection;

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

        Server.On<int, Position, Position>("TankMoved", TankMoved);
        Server.On<int>("TankShot", TankShot);
    }
    private int GetTankIdByPos(Position pos)
    {
        foreach (Tank tank in collection.Tanks)
        {
            if (tank.Position.X == pos.X && tank.Position.Y == pos.Y) { return tank.Id; }
        }

        return -1;
    }

    private IView GetTankByPos(Position pos)
    {
        foreach (IView tank in ChessMaster)
        {
            if (ChessMaster.GetColumn(tank) == pos.X && ChessMaster.GetRow(tank) == pos.Y) { return tank; }
        }

        return null;
    }

    private void TankMoved(int id, Position origin, Position position)
    {
        IView tank = GetTankByPos(origin);

        Dispatcher.Dispatch(() =>
        {
            ChessMaster.SetColumn(tank, position.X);
            ChessMaster.SetRow(tank, position.Y);
            CleanOverlay();
            collection = Server.Invoke<TankCollection>("GetTanks");
        });
    }

    private void TankShot(int id)
    {
        Tank tank = collection.GetById(id);
        IView sprite = GetTankByPos(tank.Position);

        tank.Health--;

        Dispatcher.Dispatch(() =>
        {
            CleanOverlay();
            Image element = new()
            {
                WidthRequest = CELL_SIZE,
                HeightRequest = CELL_SIZE,
                Source = "boom.gif",
                Aspect = Aspect.Fill,
                BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgba(0, 0, 0, 0)
            };

            Overlay.Add(element);
            Overlay.SetColumn(element, tank.Position.X);
            Overlay.SetRow(element, tank.Position.Y);
            Thread.Sleep(1000);

            collection = Server.Invoke<TankCollection>("GetTanks");

        });

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
        UserName.Text = string.Empty;
        Health.Text = string.Empty;
        Shoot.IsVisible = false;
    }

    private void BattlegroundReleased(object sender, EventArgs e)
    {
        Log($"Battlegrounds new position is X: {battleground.TranslationX} Y: {battleground.TranslationY}");
        pressed = false;
    }

    private void CleanOverlay()
    {
        // This is bad and dosnt solve the issue. But im not inclined to care
        for (int j = 0; j <= 32; j++)
        {
            for (int i = 0; i < Overlay.Count; i++)
            {
                if (Overlay[i] is ImageButton || Overlay[i] is Image)
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
            collection.Tanks = Server.Invoke<TankCollection>("GetTanks").Tanks;
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
                    if (i == center && j == center) { continue; }

                    bool isMine = tank.Id == LoginPage.MyTankId;
                    bool isTank = GetTankByPos(new Position(tank.Position.X + (i - center), tank.Position.Y + (j - center))) != null;

                    ImageButton element = new()
                    {
                        WidthRequest = CELL_SIZE,
                        HeightRequest = CELL_SIZE,
                        Source = isMine ? isTank ? "cross_red.png" : "circle_green.png" : "square_green.png",
                        Aspect = Aspect.Fill,
                        BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgba(0, 0, 0, 0),
                        Opacity = isTank && isMine ? 1.0f : 0.5f
                    };


                    if (isMine && !isTank)
                    {
                        element.Clicked += (object sender, EventArgs e) =>
                        {
                            if (sender is IView view)
                            {
                                Tank tank = collection.GetById(LoginPage.MyTankId);
                                int x = Overlay.GetColumn(view);
                                int y = Overlay.GetRow(view);

                                Log("I like to move it move it");
                                Log($"X: {tank.Position.X - x}, Y: {tank.Position.Y - y}");

                                Server.Invoke("MoveTank", LoginPage.Auth, (tank.Position.X - x) * -1, (tank.Position.Y - y) * -1);
                            }
                        };
                    } else if(isTank && isMine)
                    {
                        element.Clicked += (object sender, EventArgs e) =>
                        {
                            if (sender is IView view)
                            {
                                int x = Overlay.GetColumn(view);
                                int y = Overlay.GetRow(view);
                                Server.Invoke("Shoot", LoginPage.Auth, GetTankIdByPos(new Position(x, y)));
                            }
                        };
                    }

                    try
                    {
                        Overlay.SetColumn(element, tank.Position.X + (i - center));
                        Overlay.SetRow(element, tank.Position.Y + (j - center));
                    }
                    catch (ArgumentException)
                    {
                        // Dont draw circles that are not inside the map
                    }

                    Overlay.Add(element);
                }
            }

            UserName.Text = tank.UserName;
            Health.Text = $"{tank.Health} HITPOINTS";
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