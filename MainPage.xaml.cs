using Microsoft.AspNetCore.SignalR.Client;
using tank_client.Models;

namespace tank_client;

public partial class MainPage : ContentPage
{
    private bool pressed = false;
    private bool shouldUpdateAnchor = true;
    private double anchorX = 0;
    private double anchorY = 0;

    private readonly Server server;

    public MainPage()
    {
        InitializeComponent();

        server = new Server();

        new Thread(Connect).Start();
    }

    private void Connect()
    {
        Log("Connecting...");
        server.Connect();
        Log("Connected!");
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

        //movable.TranslationX = pos.Value.X - anchorX;
        //movable.TranslationY = pos.Value.Y - anchorY;
    }

    private void BattlegroundPressed(object sender, EventArgs e)
    {
        shouldUpdateAnchor = true;
        pressed = true;
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

    private class Tank
    {
        public int Id { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public int ActionPoints { get; set; }
        public int Color { get; set; }
        public Position Position { get; set; }
    }

    private class TankCollection
    {
        public int Total { get; set; }
        public Tank[] Tanks { get; set; }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        TankCollection collection = server.Invoke<TankCollection>("GetTanks");

        Log($"Total tanks: {collection.Total}");
    }

    public void Log(string msg)
    {
        // Make sure code gets run on the UI's dispatcher thread
        Dispatcher.Dispatch(() => {
            logView.Text = msg + "\n" + logView.Text;
        });
    }
}