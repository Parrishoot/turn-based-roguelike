using UnityEngine;

[CreateAssetMenu(fileName = "PushAbility", menuName = "Abilities/Active/PushAbility", order = 1)]
public class PushAbility : ActiveAbility
{
    [field:SerializeReference]
    public int Range { get; private set;}

    [field:SerializeReference]
    public int NumTargets { get; private set;}

    public override string GetAbilityDescription()
    {
        return string.Format("Push {0} target(s) up to {1} range", NumTargets, Range);
    }

    public override ActiveAbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        ISelectionController selectionController = characterManager.GetAbilitySelectionController(GetAbilitySelectionCriteria(characterManager));
        SelectionProcessor selectionProcessor = new PushSelectionProcessor(characterManager, NumTargets, Range);

        return new SelectionAbilityProcessor(characterManager, selectionController, selectionProcessor);
    }

    public override AbilitySelectionCriteria GetDefaultAbilitySelectionCriteria(CharacterManager characterManager)
    {
        return new AbilitySelectionCriteria()
            .WithAbilityType(AbilityType.OFFENSIVE);
    }
}