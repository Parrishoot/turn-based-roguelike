using TMPro;
using UnityEngine;

public class StatusEffectAttributeUIController : CharacterAttributesUIController
{
    [SerializeField]
    private TMP_Text statusEffectText;

    public StatusEffectController Controller { get; private set; }

    public void Init(StatusEffectController statusEffectController) {
        Controller = statusEffectController;
        statusEffectText.text = Controller.Perpetual ? "Perpetual: " : " This Turn: ";
        statusEffectText.text += Controller.EffectType.ToString(); 
    }


}
