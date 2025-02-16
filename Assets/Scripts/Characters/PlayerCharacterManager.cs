using UnityEngine;

public class PlayerCharacterManager : CharacterManager
{
    public override CharacterType GetCharacterType() => CharacterType.PLAYER;

    public override SelectionController GetSelectionController()
    {
        return new PlayerSelectionController();
    }
}
