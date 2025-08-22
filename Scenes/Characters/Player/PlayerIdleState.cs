using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{


    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.stateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustReleased(GameConstants.INPUT_DASH))
        {
            characterNode.stateMachineNode.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        base.EnterState();
        characterNode.animationPlayerNode.Play(GameConstants.PLAYER_ANIMATION_IDLE);
    }
}
