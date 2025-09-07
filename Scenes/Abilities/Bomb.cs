using DungeonRpg.Scenes.Constants;
using Godot;
using System;

public partial class Bomb : Ability
{
    public override void _Ready()
    {
        playerNode.AnimationFinished += HandleExpandAnimationFinished;
    }

    private void HandleExpandAnimationFinished(StringName animName)
    {
        if (animName == GameConstants.ANIMATION_EXPAND)
        {
            playerNode.Play(GameConstants.ANIMATION_EXPLOSION);
        }
        else if (animName == GameConstants.ANIMATION_EXPLOSION)
        {
            QueueFree();
        }
    }
}
