using System;

public abstract class ActiveAbilityProcessor
{
    protected CharacterManager CharacterManager { get; private set; }

    protected ActiveAbilityProcessor(CharacterManager characterManager)
    {
        this.CharacterManager = characterManager;
    }

    public Action OnAbilityFinish { get; set; }

    public abstract void Process();
}
