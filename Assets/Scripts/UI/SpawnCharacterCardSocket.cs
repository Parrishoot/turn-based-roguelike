using TMPro.EditorUtilities;
using UnityEngine;

public class SpawnCharacterCardSocket : CardDraggableSocket
{
    public override void ProcessCardInserted(CardUIController card)
    {
        // TODO: Spawn based on card type
        PlayerSelectionController playerSelectionController = new PlayerSelectionController();
        playerSelectionController.BeginSelection(new SpawnSelectionProcessor());

        card.DiscardCard();
    }
}
