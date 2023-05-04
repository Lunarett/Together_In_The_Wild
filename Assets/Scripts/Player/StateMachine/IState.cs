using UnityEngine;

    // Enum containg all states. This will be used to determine which state to switch to..
    public enum EStateID
    {
        Idle,
        KeyMovement,
        ClickMovement,
        Interact
    }

    public interface IState
    {
        EStateID GetStateID();
        void BeginState(PlayerController controller);
        void UpdateState(PlayerController controller);
        void EndState(PlayerController controller);
    }