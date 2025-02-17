using UnityEngine;

[CreateAssetMenu(fileName = "MovementAbility", menuName = "Abilities/MovementAbility", order = 1)]
public class MovementAbility : Ability
{
    [field:SerializeReference]
    public int Range { get; private set;}

    protected override AbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        return new SelectionAbilityProcessor(characterManager, new MovementSelectionProcessor(characterManager, Range));
    }
}
