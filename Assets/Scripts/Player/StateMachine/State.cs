using UnityEngine;

// Base abstract class that holds common methods and variables for different states
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
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

    protected bool IsInRange(Vector3 posA, Vector3 posB, float radius)
    {
        return Vector3.Distance(posA, posB) < radius;
    }
}
