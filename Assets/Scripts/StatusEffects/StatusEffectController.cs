using UnityEngine;

public abstract class StatusEffectController
{
    public abstract StatusEffectType EffectType { get; }

    public abstract bool Perpetual { get; }

    public virtual bool Negative => true;

    public virtual bool Stackable => false;

    protected CharacterManager CharacterManager { get; private set; }

    protected StatusEffectController(CharacterManager characterManager)
    {
        CharacterManager = characterManager;
    }

    public abstract void Apply();

    public abstract void Remove();
}
