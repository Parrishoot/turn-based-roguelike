using System;
using UnityEngine;

public abstract class ActiveAbility : ScriptableObject
{
    public void ProcessAbility(CharacterManager characterManager) {
        GetAbilityProcessor(characterManager).Process();
    }

    public abstract AbilityProcessor GetAbilityProcessor(CharacterManager characterManager);

    public abstract string GetAbilityDescription();
}
