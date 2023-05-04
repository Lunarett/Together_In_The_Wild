using UnityEngine;
using PulsarDevKit.Scripts.Debug;

public class ClickMovementState : State
{
    private float _radius = 1.0f;
    Vector2 _targetLocation;

    public override EStateID GetStateID()
    {
        return EStateID.ClickMovement;
    }

    public override void BeginState(PlayerController controller)
    {
        _targetLocation = controller.transform.position;
    
        // Check if clicked on a Interactable object
        RaycastHit2D hit = Physics2D.Raycast(GetMouseWorldLocation(controller.PlayerCamera), Vector2.zero);
        if(hit.collider == null) return;
        IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
        if (interactable == null) return;
        controller.StateMachine.ChangeState(EStateID.Interact);
    }

    public override void UpdateState(PlayerController controller)
    {
        controller.ActionHandler.ExecuteAction(EActionState.Walk);

        if(Input.GetMouseButton(0))
        {
            _targetLocation = GetMouseWorldLocation(controller.PlayerCamera);
        }

        // Handle Click movement
        controller.SetTargetLocation(_targetLocation);

        // Return to Idle state if not holding the click button
        if (IsInRange(controller.transform.position, _targetLocation, 1.0f)) controller.StateMachine.ChangeState(EStateID.Idle);
        if (IsUsingKeys()) controller.StateMachine.ChangeState(EStateID.KeyMovement);
    }

    public override void EndState(PlayerController controller)
    {
    }
}
