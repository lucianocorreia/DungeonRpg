using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();

        destination = GetPointGlobalPosition(0);
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_MOVE);

        characterNode.NavigationAgentNode.TargetPosition = destination;
        characterNode.ChaseAreaNode.BodyEntered += OnChaseAreaBodyEntered;
    }

    override protected void ExitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= OnChaseAreaBodyEntered;
    }

    override public void _PhysicsProcess(double delta)
    {
        if (characterNode.NavigationAgentNode.IsNavigationFinished())
        {
            characterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        }

        Move();
    }
}
