using UnityEngine;

[CreateAssetMenu(fileName = "MovementAbility", menuName = "Abilities/MovementAbility", order = 1)]
public class MovementAbility : Ability
{
    [field:SerializeReference]
    public int Range { get; private set;}

    public override string GetAbilityDescription()
    {
        return string.Format("Move up to {0} space(s)", Range);
    }

    public override AbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        ISelectionController selectionController = characterManager.GetSelectionController();
        SelectionProcessor selectionProcessor = new MovementSelectionProcessor(characterManager, Range);

        return new SelectionAbilityProcessor(characterManager, selectionController, selectionProcessor);
    }
}
