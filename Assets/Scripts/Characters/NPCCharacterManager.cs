using UnityEditor.UIElements;
using UnityEngine;

public abstract class NPCCharacterManager : CharacterManager
{
    [SerializeField]
    private ISelectionController selectionControllerOverride;

    [field:SerializeReference]
    public NPCAbilityManager AbilityManager { get; private set; }


    public override ISelectionController GetAbilitySelectionController(AbilitySelectionCriteria abilitySelectionCriteria)
    {
        return selectionControllerOverride == null ? new EnemyAbilitySelectionController(this, abilitySelectionCriteria) : selectionControllerOverride;
    }
}
