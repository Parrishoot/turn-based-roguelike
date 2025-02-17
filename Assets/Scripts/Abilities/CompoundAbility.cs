using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompoundAbility", menuName = "Abilities/CompoundAbility", order = 1)]
public class CompoundAbility : Ability
{
    [field:SerializeReference]
    public List<Ability> Abilites { get; private set; }

    public override AbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        return new CompoundAbilityProcessor(characterManager, Abilites);
    }
}
