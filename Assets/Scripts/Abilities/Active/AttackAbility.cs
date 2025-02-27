using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAbility", menuName = "Abilities/Active/AttackAbility", order = 1)]
public class AttackAbility : ActiveAbility
{
    [field:SerializeReference]
    public int Range { get; private set;}

    [field:SerializeReference]
    public int Damage { get; private set;}

    [field:SerializeReference]
    public int NumTargets { get; private set;}

    public override string GetAbilityDescription()
    {
        return string.Format("Attack {0} target(s) up to +{1} range for +{2} damage", NumTargets, Range, Damage);
    }

    public override AbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        ISelectionController selectionController = characterManager.GetSelectionController();
        SelectionProcessor selectionProcessor = new AttackSelectionProcessor(characterManager, NumTargets, Damage, Range);

        return new SelectionAbilityProcessor(characterManager, selectionController, selectionProcessor);
    }
}
