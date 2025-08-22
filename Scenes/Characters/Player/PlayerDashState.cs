using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class PlayerDashState : Node
{
    private Player characterNode;
    [Export] private Timer dashTimerNode;
    [Export] private float speed = 10;

    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
        dashTimerNode.Timeout += OnDashTimerTimeout;
        // SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == 5001)
        {
            characterNode.animationPlayerNode.Play(GameConstants.PLAYER_ANIMATION_DASH);
            characterNode.Velocity = new(
                characterNode.direction.X, 0, characterNode.direction.Y
            );

            if (characterNode.Velocity == Vector3.Zero)
            {
                characterNode.Velocity = characterNode.spriteNode.FlipH
                    ? Vector3.Left
                    : Vector3.Right;
            }


            characterNode.Velocity *= speed;
            dashTimerNode.Start();

            // SetPhysicsProcess(true);
        }
        else if (what == 5002)
        {

            // SetPhysicsProcess(false);
        }
    }

    private void OnDashTimerTimeout()
    {
        characterNode.Velocity = Vector3.Zero;
        characterNode.stateMachineNode.SwitchState<PlayerIdleState>();
    }
}
