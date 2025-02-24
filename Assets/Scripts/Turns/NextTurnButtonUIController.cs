using UnityEngine;
using UnityEngine.UI;

public class NextTurnButtonUIController : MonoBehaviour
{
    [SerializeField]
    private Button nextTurnButton;

    void Start()
    {
        nextTurnButton.interactable = false;

        TurnMasterManager.Instance.OnTurnStarted.OnEvery((turnType) => {
            nextTurnButton.interactable = turnType == TurnType.PLAYER;
        });
    } 
}
