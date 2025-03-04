using System;

public abstract class AbilityProcessor
{
    protected CharacterManager CharacterManager { get; private set; }

    protected AbilityProcessor(CharacterManager characterManager)
    {
        this.CharacterManager = characterManager;
    }

    public Action OnAbilityFinish { get; set; }

    public abstract void Process();
}
