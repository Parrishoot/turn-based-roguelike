public abstract class State : GenericState<StateMachine>
{
    protected State(StateMachine stateMachine) : base(stateMachine)
    {
    }
}