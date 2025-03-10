using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CompoundAbility", menuName = "Abilities/Active/CompoundAbility", order = 1)]
public class CompoundAbility : ActiveAbility
{
    [field:SerializeReference]
    public List<ActiveAbility> Abilites { get; private set; }

    public override string GetAbilityDescription()
    {
        return string.Join(", then ", Abilites.Select(x => x.GetAbilityDescription()));
    }

    public override AbilitySelectionCriteria GetDefaultAbilitySelectionCriteria(CharacterManager characterManager)
    {
        AbilitySelectionCriteria abilitySelectionCriteria = new AbilitySelectionCriteria()
            .WithAbilityType(AbilityType.SUPPORT);

        foreach (ActiveAbility ability in Abilites) {

            switch (ability.GetDefaultAbilitySelectionCriteria(characterManager).CurrentAbilityType) {
                
                // If a compound ability contains an offensive ability,
                // it is considered offensive
                case AbilityType.OFFENSIVE:
                    abilitySelectionCriteria.WithOverallAbilityType(AbilityType.OFFENSIVE);
                    break;

                // Otherwise, if it's not currently an offensive ability
                // consider a defensive ability if it contains a defensive
                // ability
                case AbilityType.SUPPORT:
                    if(abilitySelectionCriteria.OverallAbilityType != AbilityType.OFFENSIVE) {
                        abilitySelectionCriteria.WithOverallAbilityType(AbilityType.OFFENSIVE);
                    }
                    break; 
            }

            if(ability.GetDefaultAbilitySelectionCriteria(characterManager).RangeToTarget > abilitySelectionCriteria.RangeToTarget) {
                abilitySelectionCriteria.WithRangeToTarget(ability.GetDefaultAbilitySelectionCriteria(characterManager).RangeToTarget);
            }

        }

        return abilitySelectionCriteria;
    }

    public override ActiveAbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        return new CompoundAbilityProcessor(characterManager, Abilites, GetAbilitySelectionCriteria(characterManager));
    }
}
