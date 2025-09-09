using DungeonRpg.Scenes.Constants;
using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
    [Export]
    private Node currentState;

    [Export]
    private CharacterState[] states;

    public override void _Ready()
    {
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        CharacterState newState = states.FirstOrDefault(state => state is T);

        if (newState == null) { return; }

        if (currentState is T) { return; }

        if (!newState.CanTransition()) { return; }

        currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        currentState = newState;
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
