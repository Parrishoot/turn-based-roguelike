using System;
using System.Collections.Generic;

public class StatusEffectPassiveController : PlayerCharacterPassiveController
{
    private List<StatusEffectType> statusEffects;

    public StatusEffectPassiveController(List<StatusEffectType> statusEffects, Predicate<PlayerCharacterManager> filter = null) : base(filter)
    {
        this.statusEffects = statusEffects;
    }

    protected override void ProcessApplication(PlayerCharacterManager characterManager)
    {
        foreach(StatusEffectType effect in statusEffects) {
            characterManager.ApplyStatus(effect);
        }
    }

    protected override void ProcessRemoval(PlayerCharacterManager characterManager)
    {
        foreach(StatusEffectType effect in statusEffects) {
            characterManager.RemoveStatus(effect);
        }
    }
}
