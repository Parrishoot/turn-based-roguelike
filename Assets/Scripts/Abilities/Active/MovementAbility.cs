using UnityEngine;

[CreateAssetMenu(fileName = "MovementAbility", menuName = "Abilities/Active/MovementAbility", order = 1)]
public class MovementAbility : ActiveAbility
{
[field:SerializeReference]
public int Range { get; private set;}

    public override string GetAbilityDescription()
    {
        return string.Format("Move up to +{0} space(s)", Range);
    }

    public override ActiveAbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        ISelectionController selectionController = characterManager.GetAbilitySelectionController(GetAbilitySelectionCriteria(characterManager));
        SelectionProcessor selectionProcessor = new MovementSelectionProcessor(characterManager, Range);

        return new SelectionAbilityProcessor(characterManager, selectionController, selectionProcessor);
    }

    public override AbilitySelectionCriteria GetDefaultAbilitySelectionCriteria(CharacterManager characterManager)
    {
        return new AbilitySelectionCriteria()
            .WithAbilityType(AbilityType.MOVEMENT);
    }
}
