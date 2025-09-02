using Godot;
using System;

public partial class Camera3d : Camera3D
{
    [Export] private Node target;
    [Export] private Vector3 positionFromTarget;

    public override void _Ready()
    {
        GameEvents.OnGameStart += HandleGameStart;
        GameEvents.OnGameEnd += HandleGameEnd;
    }

    private void HandleGameStart()
    {
        Reparent(target);
        Position = positionFromTarget;
    }

    private void HandleGameEnd()
    {
        Reparent(GetTree().CurrentScene);
    }

}
