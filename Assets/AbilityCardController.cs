using TMPro;
using UnityEngine;

public class AbilityCardController : MonoBehaviour
{
    [SerializeField]
    private Ability ability;

    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text descriptionText;

    void Start()
    {
        titleText.text = ability.AbilityName;
        descriptionText.text = ability.GetAbilityDescription();
    }

    public void UseAbility() {
        PlayerCharacterManager playerCharacterManager = FindAnyObjectByType<PlayerCharacterManager>();
        ability.GetAbilityProcessor(playerCharacterManager).Process();
    }
}