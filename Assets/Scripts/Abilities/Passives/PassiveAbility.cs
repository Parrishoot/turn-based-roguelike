using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility : Ability
{
    public abstract PassiveController GetController(List<PlayerClass> applicableClasses);
}
