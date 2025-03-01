using System;
using UnityEngine;

public abstract class ActiveAbility : Ability
{
    public void ProcessAbility(CharacterManager characterManager) {
        GetAbilityProcessor(characterManager).Process();
    }

    public abstract AbilityProcessor GetAbilityProcessor(CharacterManager characterManager);
}
