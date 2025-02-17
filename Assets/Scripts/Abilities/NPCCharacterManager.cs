using UnityEngine;

public class NPCCharacterManager : CharacterManager
{
    [SerializeField]
    private CharacterType characterType = CharacterType.ENEMY;

    [SerializeField]
    private ISelectionController selectionControllerOverride;

    public override CharacterType GetCharacterType()
    {
        return characterType;
    }

    public override ISelectionController GetSelectionController()
    {
        return selectionControllerOverride == null ? new RandomSelectionController() : selectionControllerOverride;
    }
}
