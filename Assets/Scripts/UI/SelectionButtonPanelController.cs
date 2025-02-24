using UnityEngine;

public class SelectionButtonPanelController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectionManager.Instance.OnSelectionCompleted.OnEvery((selections) => Deactivate());
        SelectionManager.Instance.OnSelectionCanceled.OnEvery(Deactivate);
        SelectionManager.Instance.OnSelectionStarted.OnEvery(Activate);

        gameObject.SetActive(false);
    }

    private void Activate() {
        gameObject.SetActive(true);
    }

    private void Deactivate() {
        gameObject.SetActive(false);
    }
}
