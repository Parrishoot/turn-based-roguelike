using UnityEngine;

/// <summary>
/// Lose one health at the beginning of turn
/// </summary>
public class BleedController : StatusEffectController
{
    public override StatusEffectType EffectType => StatusEffectType.BLEED;

    public override bool Perpetual => true;

    private EventHandler ProcessAbilityHandler;

    public BleedController(CharacterManager characterManager) : base(characterManager)
    {
        ProcessAbilityHandler = new EveryEventHandler(ProcessBleed);
    }

    public override void Apply()
    {
        CharacterManager.OnTurnBegin.SubscribeHandler(ProcessAbilityHandler);
    }

    public override void Remove()
    {
        CharacterManager.OnTurnBegin.UnsubscribeHandler(ProcessAbilityHandler);
    }

    public void ProcessBleed() {
        CharacterManager.Damage(1, false);
    }
}
