using UnityEngine;

[CreateAssetMenu(fileName = "AttackAbility", menuName = "Abilities/AttackAbility", order = 1)]
public class AttackAbility : Ability
{
    [field:SerializeReference]
    public int Range { get; private set;}

    [field:SerializeReference]
    public int Damage { get; private set;}

    [field:SerializeReference]
    public int NumTargets { get; private set;}


    public override AbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        ISelectionController selectionController = characterManager.GetSelectionController();
        SelectionProcessor selectionProcessor = new AttackSelectionProcessor(characterManager, NumTargets, Damage, Range);

        return new SelectionAbilityProcessor(characterManager, selectionController, selectionProcessor);
    }
}
