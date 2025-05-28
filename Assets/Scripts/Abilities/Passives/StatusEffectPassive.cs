using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectPassive", menuName = "Abilities/Passive/Status Effect", order = 1)]
public class StatusEffectPassive : PassiveAbility
{
    [SerializeField]
    private List<StatusEffectType> effects;

    [SerializeField]
    private List<PlayerClass> classes = new List<PlayerClass>();

    public override string GetAbilityDescription()
    {
        return string.Format("Gain {0}", string.Join(",", effects));
    }

    public override PassiveController GetController(List<PlayerClass> applicableClasses)
    {
        Predicate<PlayerCharacterManager> filter = (x) => applicableClasses.Contains(x.Class);
        return new StatusEffectPassiveController(effects, filter);
    }
}
