using Godot;
using System;
using System.Reflection.Metadata;

public partial class EnemiesContainer : Node3D
{
    public override void _Ready()
    {
        int totalEnemies = GetChildCount();
        GameEvents.RaiseNewEnemyCount(totalEnemies);

        ChildExitingTree += HandleChildExitingTree;
    }

    private void HandleChildExitingTree(Node node)
    {
        int remainingEnemies = GetChildCount() - 1;
        GameEvents.RaiseNewEnemyCount(remainingEnemies);

        if (remainingEnemies <= 0)
        {
            GameEvents.RaiseVictory();
        }
    }
}
