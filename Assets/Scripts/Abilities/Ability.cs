using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [field:SerializeReference]
    public string AbilityName { get ; private set;}

    public void ProcessAbility(CharacterManager characterManager) {
        GetAbilityProcessor(characterManager).Process();
    }

    public abstract AbilityProcessor GetAbilityProcessor(CharacterManager characterManager);
}
