using TMPro;
using UnityEngine;

public class ImmunityAttributeUIController : CharacterAttributesUIController
{
    [SerializeField]
    private TMP_Text immunityText;

    public void Init(StatusEffectType statusEffectType) {
        immunityText.text = "Immune to: " + statusEffectType.ToString();
    }
}
