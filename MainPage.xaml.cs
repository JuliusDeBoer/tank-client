﻿namespace tank_client;

public partial class MainPage : ContentPage
{
    private bool pressed = false;
    private bool shouldUpdateAnchor = true;
    private double anchorX = 0;
    private double anchorY = 0;

    public MainPage()
    {
        InitializeComponent();
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
    }

    private void BattlegroundReleased(object sender, EventArgs e)
    {
        pressed = false;
    }
}