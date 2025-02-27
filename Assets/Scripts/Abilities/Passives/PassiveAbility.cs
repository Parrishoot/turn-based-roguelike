using UnityEngine;

public abstract class PassiveAbility : ScriptableObject
{
    [field:SerializeReference]
    public int Cost { get; set; }

    public abstract PassiveController GetController();
}
