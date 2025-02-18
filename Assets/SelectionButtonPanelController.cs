using UnityEngine;

public class SelectionButtonPanelController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectionManager.Instance.OnSelectionCompleted += () => gameObject.SetActive(false);
        SelectionManager.Instance.OnSelectionStarted += () => gameObject.SetActive(true);

        gameObject.SetActive(false);
    }
}
