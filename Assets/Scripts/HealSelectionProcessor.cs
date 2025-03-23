using UnityEngine;

public class HealSelectionProcessor : AllyTargetSelectionProcessor
{
    private int healAmount;

    public HealSelectionProcessor(CharacterManager characterManager, int numTargets = 1, int range = 1, int healAmount = 1) : base(characterManager, numTargets, range)
    {
        this.healAmount = healAmount;
    }

    protected override void ApplyToSelectedSpace(BoardSpace space)
    {
        space.Occupant.Heal(healAmount);
    }
}
