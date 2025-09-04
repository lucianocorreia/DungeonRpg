using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

public partial class UiController : Control
{
    private Dictionary<ContainerType, UiContainer> containers = [];

    public override void _Ready()
    {
        containers = GetChildren()
            .Where((child) => child is UiContainer)
            .Cast<UiContainer>()
            .ToDictionary((element) => element.container);

        containers[ContainerType.Start].Visible = true;
        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartButtonPressed;
    }

    private void HandleStartButtonPressed()
    {
        GetTree().Paused = false;

        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;

        GameEvents.RaiseStartGame();
    }
}
