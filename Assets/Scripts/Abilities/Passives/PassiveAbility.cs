using UnityEngine;

public abstract class PassiveAbility : Ability
{
    [field:SerializeReference]
    public int Cost { get; set; }

    public abstract PassiveController GetController();
}
