using System.Collections.Generic;
using UnityEngine;

public class NPCAbilityManager : MonoBehaviour
{
    public EventProcessor OnAbilityFinish = new EventProcessor();

    [SerializeField]
    private CharacterManager characterManager;

    [SerializeField]
    private List<Ability> abilities;
    
    public void UseAbility() {
        AbilityProcessor ability = abilities[0].GetAbilityProcessor(characterManager);
        ability.OnAbilityFinish += OnAbilityFinish.Process;

        ability.Process();
    }
}
