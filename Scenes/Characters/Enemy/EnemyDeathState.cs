using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIMATION_DEATH);
        characterNode.AnimationPlayerNode.AnimationFinished += OnDeathAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.AnimationPlayerNode.AnimationFinished -= OnDeathAnimationFinished;
    }

    private void OnDeathAnimationFinished(StringName animName)
    {
        characterNode.QueueFree();
    }
}
