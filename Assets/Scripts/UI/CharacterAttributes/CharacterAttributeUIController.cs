using UnityEngine;

public class CharacterAttributesUIController : MonoBehaviour
{
    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
