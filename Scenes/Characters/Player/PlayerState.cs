using System;
using DungeonRpg.Scenes.Constants;
using Godot;

public abstract partial class PlayerState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();

        characterNode.GetStatResource(Stat.Health).OnZero += OnHealthZeroHealth;
    }

    protected virtual void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            characterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }

    private void OnHealthZeroHealth()
    {
        characterNode.StateMachineNode.SwitchState<PlayerDeathState>();
    }


}
