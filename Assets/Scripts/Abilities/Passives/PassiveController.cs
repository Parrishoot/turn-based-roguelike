using UnityEngine;

public abstract class PassiveController 
{
    public int Cost { get; private set; }

    public bool Active { get; private set; } = false;

    protected PassiveController(int cost)
    {
        Cost = cost;
    }

    public void Activate() {
        // TODO: Add mana subtraction here
        Active = true;
        ProcessActivation();
    }

    public void Deactivate() {
        Active = false;
        ProcessDeactivation();   
    }

    protected abstract void ProcessActivation();

    protected abstract void ProcessDeactivation();
}
