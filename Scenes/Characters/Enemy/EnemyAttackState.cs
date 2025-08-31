using DungeonRpg.Scenes.Constants;
using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 targetPosition;

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_ATTACK);

        Node3D target = characterNode.AttackAreaNode
            .GetOverlappingBodies()
            .First();

        targetPosition = target.GlobalPosition;

        characterNode.AnimationPlayerNode.AnimationFinished += OnAttackAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.AnimationPlayerNode.AnimationFinished -= OnAttackAnimationFinished;
    }

    private void PerformHit()
    {
        characterNode.ToggleHitBox(false);
        characterNode.HitBoxNode.GlobalPosition = targetPosition;
    }

    private void OnAttackAnimationFinished(StringName animName)
    {
        characterNode.ToggleHitBox(true);
        Node3D target = characterNode.AttackAreaNode
            .GetOverlappingBodies()
            .FirstOrDefault();

        if (target == null)
        {
            Node3D chaseTarget = characterNode.ChaseAreaNode
                .GetOverlappingBodies()
                .FirstOrDefault();

            if (chaseTarget == null)
            {
                characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
                return;
            }

            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            return;
        }

        characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_ATTACK);
        targetPosition = target.GlobalPosition;

        Vector3 direction = characterNode.GlobalPosition.DirectionTo(targetPosition);
        characterNode.SpriteNode.FlipH = direction.X < 0;
    }

}
