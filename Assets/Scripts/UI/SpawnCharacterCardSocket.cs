using TMPro.EditorUtilities;
using UnityEngine;

public class SpawnCharacterCardSocket : CardDraggableSocket
{
    [SerializeField]
    private int baseCost = 15;

    public override void ProcessCardInserted(CardUIController card)
    {
        if(!ManaGer.Instance.ManaAvailable(baseCost)) {
            return;
        }

        // TODO: Spawn based on card type
        PlayerSelectionController playerSelectionController = new PlayerSelectionController();
        playerSelectionController.BeginSelection(new SpawnSelectionProcessor());

        card.DiscardCard();
        ManaGer.Instance.SpendMana(baseCost);
    }
}
