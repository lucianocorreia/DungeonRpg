using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export]
    public AnimationPlayer animationPlayerNode;
    [Export]
    public Sprite3D spriteNode;
    [Export]
    public StateMachine stateMachineNode;

    public Vector2 direction = new();

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = direction.X == 0;

        if (isNotMovingHorizontally) { return; }

        bool isMovingLeft = direction.X < 0;
        spriteNode.FlipH = isMovingLeft;
    }
}
