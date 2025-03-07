using UnityEngine;

public class PlayerCharacterManager : CharacterManager
{
    [field:SerializeReference]
    public CharacterClass Class { get; private set; }

    public override CharacterType GetCharacterType() => CharacterType.PLAYER;

    public override ISelectionController GetSelectionController()
    {
        return new PlayerSelectionController();
    }
}
