using UnityEngine;

public class PlayCardSocket : CardDraggableSocket
{
    [SerializeField]
    private CharacterManager characterManager;

    public override void ProcessCardInserted(CardUIController card)
    {
        card.UseActive(characterManager);
    }
}
