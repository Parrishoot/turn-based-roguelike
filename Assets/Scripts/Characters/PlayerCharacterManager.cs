using UnityEngine;

public class PlayerCharacterManager : CharacterManager
{
    [field:SerializeReference]
    public PlayerClass Class { get; private set; }

    public override CharacterType GetCharacterType() => CharacterType.PLAYER;

    public override ISelectionController GetAbilitySelectionController(AbilitySelectionCriteria abilitySelectionCriteria)
    {
        return new PlayerSelectionController();
    }
}
