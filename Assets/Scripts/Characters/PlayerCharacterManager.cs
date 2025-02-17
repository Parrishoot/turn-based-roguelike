using UnityEngine;

public class PlayerCharacterManager : CharacterManager
{
    public override CharacterType GetCharacterType() => CharacterType.PLAYER;

    public override ISelectionController GetSelectionController()
    {
        return new PlayerSelectionController();
    }
}
