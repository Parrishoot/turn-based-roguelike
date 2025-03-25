using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class CompoundAbilityNode
{
    public CompoundAbilityNode(ActiveAbility ability, bool chain)
    {
        Ability = ability;
        Chain = chain;
    }

    [field:SerializeField]
    public ActiveAbility Ability { get; private set; }

    [field: SerializeField]
    public bool Chain { get; private set; } = true;
}
