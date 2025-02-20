using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityCardController : MonoBehaviour
{

    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text descriptionText;

    private Ability ability;

    public void Setup(Ability ability)
    {
        this.ability = ability;
        titleText.text = ability.AbilityName;
        descriptionText.text = ability.GetAbilityDescription();
    }

    public void UseAbility() {
        PlayerCharacterManager playerCharacterManager = FindAnyObjectByType<PlayerCharacterManager>();

        AbilityProcessor abilityProcessor = ability.GetAbilityProcessor(playerCharacterManager);
        abilityProcessor.OnAbilityFinish += DiscardCard;

        abilityProcessor.Process();
    }

    private void DiscardCard()
    {
        DeckManager.Instance.DiscardAbility(ability);
        Destroy(gameObject);
    }
}