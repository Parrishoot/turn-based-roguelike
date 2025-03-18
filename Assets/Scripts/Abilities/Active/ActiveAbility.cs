using System;
using UnityEngine;

public abstract class ActiveAbility : Ability
{
    public void ProcessAbility(CharacterManager characterManager) {
        GetAbilityProcessor(characterManager).Process();
    }

    public AbilitySelectionCriteria AbilitySelectionCriteriaOverride { get; set; }

    public AbilitySelectionCriteria GetAbilitySelectionCriteria(CharacterManager characterManager) {
        return AbilitySelectionCriteriaOverride == null ? GetDefaultAbilitySelectionCriteria(characterManager) : AbilitySelectionCriteriaOverride;
    }

    public abstract ActiveAbilityProcessor GetAbilityProcessor(CharacterManager characterManager);
    
    public abstract AbilitySelectionCriteria GetDefaultAbilitySelectionCriteria(CharacterManager characterManager);
}
