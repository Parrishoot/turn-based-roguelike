using UnityEngine;

public class PlayCardSocket : CardDraggableSocket
{
    [SerializeField]
    private PlayerCharacterManager playerCharacterManager;

    public override bool CanProcessCard(Card card)
    {
        return card.CanPlayOnCharacter(playerCharacterManager.Class);
    }

    public override void ProcessCardInserted(CardUIController card)
    {
        card.UseActive(playerCharacterManager);
    }
}
