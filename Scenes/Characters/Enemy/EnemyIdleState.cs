using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_IDLE);
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }
}
