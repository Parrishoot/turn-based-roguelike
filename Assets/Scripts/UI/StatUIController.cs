using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StatUIController : MonoBehaviour
{
    [SerializeField]
    TMP_Text statNameText;

    [SerializeField]
    TMP_Text statValueText;

    private CharacterStatType statType;

    private CharacterManager characterManager;

    public void Setup(CharacterManager characterManager, CharacterStatType statType) {
        this.characterManager = characterManager;
        this.statType = statType;

        statNameText.text = statType.ToString();
    }

    void Update() {
        statValueText.text = characterManager.ProfileManager.Stats[statType].CurrentValue.ToString();
    }
}
