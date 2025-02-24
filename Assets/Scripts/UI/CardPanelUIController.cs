using UnityEngine;

public class CardPanelUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPanelGameObject;

    void Start()
    {
        TurnMasterManager.Instance.OnTurnStarted.OnEvery(CheckActivate);
    }

    public void CheckActivate(TurnType turnType) {
        cardPanelGameObject.SetActive(turnType == TurnType.PLAYER);
    }
}
