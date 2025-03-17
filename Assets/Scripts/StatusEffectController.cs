using UnityEngine;

public abstract class StatusEffectController : MonoBehaviour
{
    public abstract StatusEffectType EffectType { get; }

    public virtual bool Stackable => false;

    protected CharacterManager CharacterManager { get; private set; }

    public bool Perpetual { get; private set; } = true;

    protected StatusEffectController(CharacterManager characterManager, bool perpetual=true)
    {
        CharacterManager = characterManager;
        Perpetual = perpetual;
    }

    public abstract void Apply();

    public abstract void Remove();
}
