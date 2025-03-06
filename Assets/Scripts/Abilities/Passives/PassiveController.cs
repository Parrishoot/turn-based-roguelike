using UnityEngine;

public abstract class PassiveController 
{
    public bool Active { get; private set; } = false;

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
