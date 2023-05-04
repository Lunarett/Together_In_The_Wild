using UnityEngine;
using PulsarDevKit.Scripts.Debug;

public class IdleState : State
{
    public override EStateID GetStateID()
    {
        return EStateID.Idle;
    }

    public override void BeginState(PlayerController controller)
    {
    }

    public override void UpdateState(PlayerController controller)
    {
        controller.ActionHandler.ExecuteAction(EActionState.Idle);

        // Switch state if using movement keys to KeyMovementState
        if(IsUsingKeys()) controller.StateMachine.ChangeState(EStateID.KeyMovement);

        // Switch state if clicking to ClickMovementState
        if(Input.GetMouseButton(0)) controller.StateMachine.ChangeState(EStateID.ClickMovement);
    }

    public override void EndState(PlayerController controller)
    {
    }
}
