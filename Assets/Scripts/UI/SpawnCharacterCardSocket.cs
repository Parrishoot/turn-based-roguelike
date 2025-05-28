using TMPro.EditorUtilities;
using UnityEngine;

public class SpawnCharacterCardSocket : CardDraggableSocket
{
    [SerializeField]
    private int baseCost = 15;

    public override bool CanProcessCard(Card card)
    {
        return ManaGer.Instance.ManaAvailable(baseCost) && card.GetSpawnPlayerClass().HasValue;
    }

    public override void ProcessCardInserted(CardUIController card)
    {
        if(!CanProcessCard(card.Card)) {
            return;
        }

        PlayerClass? spawnClass = card.Card.GetSpawnPlayerClass();

        if (!spawnClass.HasValue)
        {
            return;
        }

        PlayerSelectionController playerSelectionController = new PlayerSelectionController();
        playerSelectionController.BeginSelection(new SpawnSelectionProcessor(spawnClass.Value));

        card.DiscardCard();
        ManaGer.Instance.SpendMana(baseCost);
    }
}
