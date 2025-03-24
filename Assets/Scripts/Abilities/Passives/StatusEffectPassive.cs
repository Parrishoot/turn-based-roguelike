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

    protected List<PlayerClass> ApplicableClasses {
        get {
            if(classes.Count == 0) {
                List<PlayerClass> applicableClasses = Enum.GetValues(typeof(PlayerClass)).Cast<PlayerClass>().ToList();
                applicableClasses.Remove(PlayerClass.TOTEM);

                return applicableClasses;
            }

            return classes;
        }
    }

    public override string GetAbilityDescription()
    {
        return string.Format("{0} gains {1}", string.Join(",", ApplicableClasses), string.Join(",", effects));
    }

    public override PassiveController GetController()
    {
        return new StatusEffectPassiveController(effects, Filter);
    }

    private bool Filter(PlayerCharacterManager characterManager) { 
        return ApplicableClasses.Contains(characterManager.Class);
    }
}
