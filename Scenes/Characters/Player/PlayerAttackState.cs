using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboTimerNode;

    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();

        comboTimerNode.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(
            GameConstants.ANIMATION_ATTACK + comboCounter,
            -1,
            1.5f
        );

        characterNode.AnimationPlayerNode.AnimationFinished += OnAttackAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.AnimationPlayerNode.AnimationFinished -= OnAttackAnimationFinished;
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

}
