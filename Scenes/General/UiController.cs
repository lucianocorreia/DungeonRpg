using DungeonRpg.Scenes.Constants;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

public partial class UiController : Control
{
    private Dictionary<ContainerType, UiContainer> containers = [];

    private bool canPause = false;

    public override void _Ready()
    {
        containers = GetChildren()
            .Where((child) => child is UiContainer)
            .Cast<UiContainer>()
            .ToDictionary((element) => element.container);

        containers[ContainerType.Start].Visible = true;
        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartButtonPressed;
        containers[ContainerType.Pause].ButtonNode.Pressed += HandlePauseButtonPressed;

        GameEvents.OnGameEnd += HandleGameEnd;
        GameEvents.OnVictory += HandleVictory;
    }

    public override void _Input(InputEvent @event)
    {
        if (!canPause)
        {
            return;
        }

        if (!Input.IsActionPressed(GameConstants.INPUT_PAUSE))
        {
            return;
        }

        containers[ContainerType.Stats].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;
    }

    private void HandleVictory()
    {
        canPause = false;

        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Victory].Visible = true;

        GetTree().Paused = true;
    }

    private void HandleGameEnd()
    {
        canPause = false;

        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }

    private void HandleStartButtonPressed()
    {
        canPause = true;

        GetTree().Paused = false;

        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;

        GameEvents.RaiseStartGame();
    }

    private void HandlePauseButtonPressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Pause].Visible = false;
        containers[ContainerType.Stats].Visible = true;
    }

}
