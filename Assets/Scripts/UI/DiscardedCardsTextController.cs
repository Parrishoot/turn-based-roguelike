using TMPro;
using UnityEngine;

public class DeckTextUIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text discardedCardsText;

    [SerializeField]
    private TMP_Text deckCardsText;

    // Update is called once per frame
    void Update()
    {
        discardedCardsText.text = DeckManager.Instance.Graveyard.Count.ToString();
        deckCardsText.text = DeckManager.Instance.CurrentDeck.Count.ToString();
    }
}
