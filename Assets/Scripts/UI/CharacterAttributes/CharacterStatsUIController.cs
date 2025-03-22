using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class CharacterStatsUIController : CharacterAttributesUIController
{

    [SerializeField]
    private TMP_Text healthText;

    [SerializeField]
    private TMP_Text shieldText;

    [SerializeField]
    private TMP_Text rangeText;

    [SerializeField]
    private TMP_Text damageText;

    [SerializeField]
    private TMP_Text movementText;

    private CharacterManager characterManager;

    public void Init(CharacterManager characterManager) {
        this.characterManager = characterManager;
    }

    void Update()
    {
        healthText.SetText(string.Format("H: {0}", characterManager.ProfileManager.Stats[CharacterStatType.HEALTH].CurrentValue));
        shieldText.SetText(string.Format("S: {0}", characterManager.ProfileManager.Stats[CharacterStatType.SHIELD].CurrentValue));
        rangeText.SetText(string.Format("R: {0}", characterManager.ProfileManager.Stats[CharacterStatType.RANGE].CurrentValue));
        damageText.SetText(string.Format("D: {0}", characterManager.ProfileManager.Stats[CharacterStatType.DAMAGE].CurrentValue));
        movementText.SetText(string.Format("M: {0}", characterManager.ProfileManager.Stats[CharacterStatType.MOVEMENT].CurrentValue));
    }
}
