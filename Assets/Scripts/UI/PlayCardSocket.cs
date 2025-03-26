using UnityEngine;

public class PlayCardSocket : CardDraggableSocket
{
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]

    private PlayerCharacterManager playerCharacterManager;

    [SerializeField]
    private Vector3 offset = Vector2.zero;

    public void Init(PlayerCharacterManager playerCharacterManager) {
        this.playerCharacterManager = playerCharacterManager;
    }

    public override bool CanProcessCard(Card card)
    {
        return card.CanPlayOnCharacter(playerCharacterManager.Class);
    }

    public override void ProcessCardInserted(CardUIController card)
    {
        card.UseActive(playerCharacterManager);
    }

    public void Update()
    {
        rectTransform.position = Camera.main.WorldToScreenPoint(playerCharacterManager.transform.position) + offset;
    }
}
