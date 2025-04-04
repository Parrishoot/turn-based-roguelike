using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundPanelUIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text roundText;

    [SerializeField]
    private Button roundButton;

    void Start()
    {
        RoundManager.Instance.OnRoundStarted.OnEvery(() => {
            roundText.gameObject.SetActive(false);
            roundButton.gameObject.SetActive(false);
        });

        RoundManager.Instance.OnRoundLost.OnEvery(() => {
            roundText.gameObject.SetActive(true);
            roundText.text = "LOSER";

            roundButton.gameObject.SetActive(true);
        });

        RoundManager.Instance.OnRoundWon.OnEvery(() => {
            roundText.gameObject.SetActive(true);
            roundText.text = "ROUND WON";

            roundButton.gameObject.SetActive(true);
        });
    }

    public void RoundButtonPressed() {
        RoundManager.Instance.BeginRound();
    }
}
