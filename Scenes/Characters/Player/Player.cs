using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export]
    private AnimationPlayer animationPlayerNode;
    [Export]
    private Sprite3D spriteNode;

    private Vector2 direction = new();

    public override void _Ready()
    {
        animationPlayerNode.Play(GameConstants.PLAYER_ANIMATION_IDLE);
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = new(direction.X, 0, direction.Y);
        Velocity *= 5;

        MoveAndSlide();

        Flip();
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);

        if (direction == Vector2.Zero)
        {
            animationPlayerNode.Play(GameConstants.PLAYER_ANIMATION_IDLE);
        }
        else
        {
            animationPlayerNode.Play(GameConstants.PLAYER_ANIMATION_MOVE);
        }
    }

    private void Flip()
    {
        bool isNotMovingHorizontally = direction.X == 0;

        if (isNotMovingHorizontally) { return; }

        bool isMovingLeft = direction.X < 0;
        spriteNode.FlipH = isMovingLeft;
    }
}
