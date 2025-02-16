using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class CharacterAbilityManager : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager;

    [SerializeField]
    private List<Ability> abilities;
    
    [ProButton]
    private void UseAbility() {
        abilities[0].ProcessAbility(characterManager);
    }
}
