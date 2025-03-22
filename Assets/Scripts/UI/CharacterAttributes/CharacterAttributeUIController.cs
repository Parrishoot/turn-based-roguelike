using UnityEngine;

public class CharacterAttributesUIController : MonoBehaviour
{
    protected CharacterManager CharacterManager { get; private set; }

    public void Init(CharacterManager characterManager) {
        CharacterManager = characterManager;
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
