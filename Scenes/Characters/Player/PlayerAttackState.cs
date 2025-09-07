using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export]
    private Timer comboTimerNode;

    [Export]
    private PackedScene lightningScene;

    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();

        comboTimerNode.Timeout += () => comboCounter = 1;
    }

    public override void _PhysicsProcess(double delta)
    {
        // Permite movimento durante o ataque
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
            characterNode.Velocity *= 5; // Use o mesmo valor de speed do PlayerMoveState ou exporte como variável
            characterNode.MoveAndSlide();
            characterNode.Flip();
        }
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(
            GameConstants.ANIMATION_ATTACK + comboCounter,
            -1,
            1.5f
        );

        characterNode.AnimationPlayerNode.AnimationFinished += OnAttackAnimationFinished;
        characterNode.HitBoxNode.BodyEntered += HandleBodyEntered;
    }


    protected override void ExitState()
    {
        characterNode.AnimationPlayerNode.AnimationFinished -= OnAttackAnimationFinished;
        characterNode.HitBoxNode.BodyEntered -= HandleBodyEntered;
        comboTimerNode.Start();
    }

    private void OnAttackAnimationFinished(StringName animName)
    {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);

        characterNode.ToggleHitBox(true);

        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    private void PerformHit()
    {
        Vector3 newPosition = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;

        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;

        characterNode.HitBoxNode.Position = newPosition;

        characterNode.ToggleHitBox(false);
    }

    private void HandleBodyEntered(Node3D body)
    {
        if (comboCounter != maxComboCount) { return; }

        Node3D lightning = lightningScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(lightning);
        lightning.GlobalPosition = body.GlobalPosition + Vector3.Up * 2;

    }

}
