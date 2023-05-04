using UnityEngine;
using PulsarDevKit.Scripts.Debug;

public class KeyMovementState : State
{
    public override EStateID GetStateID()
    {
        return EStateID.KeyMovement;
    }

    public override void BeginState(PlayerController controller)
    {
    }

    public override void UpdateState(PlayerController controller)
    {
        controller.ActionHandler.ExecuteAction(EActionState.Walk);

        // Handle movement with keys
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 newPos = new Vector2(controller.transform.position.x, controller.transform.position.y) + dir;
        controller.SetTargetLocation(newPos);
        
        // Return to Idle state if not using keys
        if(!IsUsingKeys()) controller.StateMachine.ChangeState(EStateID.Idle);
    }

    public override void EndState(PlayerController controller)
    {
    }
}
