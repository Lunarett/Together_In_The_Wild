public class PlayerStateMachine
{
    public IState[] States;
    public PlayerController Controller;
    public EStateID CurrentStateID;

    public PlayerStateMachine(PlayerController controller)
    {
        Controller = controller;
        int len = System.Enum.GetNames(typeof(EStateID)).Length;
        States = new IState[len];
    }

    public void InitializeState(IState state)
    {
        int i = (int)state.GetStateID();
		States[i] = state;
    }

    public IState GetState(EStateID stateID)
    {
        int i = (int)stateID;
		return States[i];
    }

    public void UpdateStateMachine()
    {
        GetState(CurrentStateID)?.UpdateState(Controller);
    }

    public void ChangeState(EStateID newStateID)
    {
        GetState(CurrentStateID)?.EndState(Controller);
		CurrentStateID = newStateID;
		GetState(CurrentStateID)?.BeginState(Controller);
    }
}
