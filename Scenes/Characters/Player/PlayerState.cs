using DungeonRpg.Scenes.Constants;
using Godot;

public abstract partial class PlayerState : CharacterState
{
    protected virtual void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            characterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }

}
