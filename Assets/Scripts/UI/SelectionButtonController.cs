using UnityEngine;
using UnityEngine.UI;

public class SelectionButtonController : MonoBehaviour
{
    [SerializeField]
    private Button button;

    void Update()
    {
        button.interactable = SelectionManager.Instance.ValidSelection();
    }
}
