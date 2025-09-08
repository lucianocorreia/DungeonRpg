using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class StunState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_STUN);

        characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        base.ExitState();
        characterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (characterNode.AttackAreaNode.HasOverlappingBodies())
        {
            characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
        }
        else if (characterNode.ChaseAreaNode.HasOverlappingBodies())
        {
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }
        else
        {
            characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
        }
    }
}
