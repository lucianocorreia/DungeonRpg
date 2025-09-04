using Godot;
using System;

public partial class EnemyCountLabel : Label
{
    public override void _Ready()
    {
        base._Ready();
        GameEvents.OnNewEnemyCount += HandleUpdateEnemyCountLabel;
    }

    private void HandleUpdateEnemyCountLabel(int count)
    {
        Text = $"{count}";
    }
}
