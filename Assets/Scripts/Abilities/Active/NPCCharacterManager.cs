using UnityEditor.UIElements;
using UnityEngine;

public class NPCCharacterManager : CharacterManager
{
    [SerializeField]
    private CharacterType characterType = CharacterType.ENEMY;

    [SerializeField]
    private ISelectionController selectionControllerOverride;

    [field:SerializeReference]
    public NPCAbilityManager AbilityManager { get; private set; }

    public override CharacterType GetCharacterType()
    {
        return characterType;
    }

    public override ISelectionController GetAbilitySelectionController(AbilitySelectionCriteria abilitySelectionCriteria)
    {
        return selectionControllerOverride == null ? new EnemyAbilitySelectionController(this, abilitySelectionCriteria) : selectionControllerOverride;
    }
}
