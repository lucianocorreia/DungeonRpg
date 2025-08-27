using DungeonRpg.Scenes.Constants;
using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    [Export] private Timer timerNode;

    private CharacterBody3D target;

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_MOVE);
        target = characterNode
            .ChaseAreaNode
            .GetOverlappingBodies()
            .First() as CharacterBody3D;

        timerNode.Timeout += OnTimerTimeout;
    }

    protected override void ExitState()
    {
        timerNode.Timeout -= OnTimerTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    private void OnTimerTimeout()
    {
        destination = target.GlobalPosition;
        characterNode.NavigationAgentNode.TargetPosition = destination;

    }

}
