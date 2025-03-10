using System.Collections.Generic;
using UnityEngine;

public class NPCAbilityManager : MonoBehaviour
{
    public EventProcessor OnAbilityFinish = new EventProcessor();

    [SerializeField]
    private CharacterManager characterManager;

    [SerializeField]
    private List<ActiveAbility> abilities;
    
    public void UseAbility() {
        ActiveAbilityProcessor ability = abilities[0].GetAbilityProcessor(characterManager);
        ability.OnAbilityFinish += OnAbilityFinish.Process;

        ability.Process();
    }
}
