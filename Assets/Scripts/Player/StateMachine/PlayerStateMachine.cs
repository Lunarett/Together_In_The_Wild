using UnityEngine;

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

public abstract class State : IState
{
    public abstract EStateID GetStateID();
    public abstract void BeginState(PlayerController controller);
    public abstract void UpdateState(PlayerController controller);
    public abstract void EndState(PlayerController controller);

    protected Vector2 GetMouseWorldLocation(Camera camera)
    {
        Vector3 mousePosWorld = camera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePosWorld.x, mousePosWorld.y);
    }

    protected bool IsUsingKeys()
    {
        return !Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0f) || !Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0f);
    }

    protected bool IsInRange(Vector3 posA, Vector3 posB, float radius)
    {
        return Vector3.Distance(posA, posB) < radius;
    }
}

public class PlayerStateMachine
{
    private IState[] states;
    private PlayerController controller;
    private EStateID currentStateID;

    public PlayerStateMachine(PlayerController controller)
    {
        this.controller = controller;
        states = new IState[System.Enum.GetValues(typeof(EStateID)).Length];
    }

    public void InitializeState(IState state)
    {
        states[(int)state.GetStateID()] = state;
    }

    public IState GetState(EStateID stateID)
    {
        return states[(int)stateID];
    }

    public void UpdateStateMachine()
    {
        GetState(currentStateID)?.UpdateState(controller);
    }

    public void ChangeState(EStateID newStateID)
    {
        GetState(currentStateID)?.EndState(controller);
        currentStateID = newStateID;
        GetState(currentStateID)?.BeginState(controller);
    }
}