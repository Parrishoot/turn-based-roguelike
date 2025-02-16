public class WaitState : TimedState
{
    private State nextState;

    public WaitState(StateMachine stateMachine, float time, State nextState) : base(stateMachine, time)
    {
        this.nextState = nextState;
    }

    public override void OnEnd()
    {
        
    }

    protected override void OnTimerEnd()
    {
        StateMachine.ChangeState(nextState);
    }
}