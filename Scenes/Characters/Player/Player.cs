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
        animationPlayerNode.Play("Idle");
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = new(direction.X, 0, direction.Y);
        Velocity *= 5;

        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector("MoveLeft", "MoveRight", "MoveForward", "MoveBackward");

        if (direction == Vector2.Zero)
        {
            animationPlayerNode.Play("Idle");
        }
        else
        {
            animationPlayerNode.Play("Move");
        }
    }
}
