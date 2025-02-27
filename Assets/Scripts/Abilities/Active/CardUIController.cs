using TMPro;
using UnityEngine;

public class CardUIController : MonoBehaviour
{

    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text descriptionText;

    private Card card;

    private PassiveController passive = null;

    public void Setup(Card card)
    {
        this.card = card;
        titleText.text = card.CardName;
        descriptionText.text = card.Active.GetAbilityDescription();
    }

    public void UseActive() {

        if(passive != null && passive.Active) {
            return;
        }

        PlayerCharacterManager playerCharacterManager = FindAnyObjectByType<PlayerCharacterManager>();

        AbilityProcessor abilityProcessor = card.Active.GetAbilityProcessor(playerCharacterManager);
        abilityProcessor.OnAbilityFinish += DiscardCard;

        abilityProcessor.Process();
    }

    public void TogglePassive() {

        if(passive == null || !passive.Active) {
            passive = card.Passive.GetController();
            passive.Activate();
        }
        else {
            passive.Deactivate();
            DiscardCard();
        }
    }

    private void DiscardCard()
    {
        DeckManager.Instance.DiscardAbility(card);
        Destroy(gameObject);
    }
}