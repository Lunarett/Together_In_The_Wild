using UnityEngine;

public class InteractState : State
{
    private GameObject _interactableObject;
    private bool _doOnce;

    public override EStateID GetStateID()
    {
        return EStateID.Interact;
    }

    public override void BeginState(PlayerController controller)
    {
        _interactableObject = Physics2D.Raycast(GetMouseWorldLocation(controller.PlayerCamera), Vector2.zero).collider.gameObject;
    }

    public override void UpdateState(PlayerController controller)
    {
        controller.SetTargetLocation(_interactableObject.transform.position);

        if(IsInRange(controller.transform.position, _interactableObject.transform.position, 1.0f))
        {
            if(!_doOnce)
            {
                _doOnce = true;
                controller.ActionHandler.ExecuteAction(EActionState.Pickup);
            }

            if(!controller.ActionHandler.IsInAction)
            {
                IInteractable interactable = _interactableObject.GetComponent<IInteractable>();
                interactable.Interact(controller);
                _doOnce = false;
                controller.StateMachine.ChangeState(EStateID.Idle);
            }
        }

        if(IsUsingKeys() && !controller.ActionHandler.IsInAction) controller.StateMachine.ChangeState(EStateID.KeyMovement);
        if(Input.GetMouseButton(0) && !controller.ActionHandler.IsInAction) controller.StateMachine.ChangeState(EStateID.ClickMovement);
    }

    public override void EndState(PlayerController controller)
    {
    }
}
