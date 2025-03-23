using UnityEngine;

public class AttackSelectionProcessor : EnemyTargetSelectionProcessor
{
    private int damage = 1;

    public AttackSelectionProcessor(CharacterManager characterManager, int numTargets = 1, int damage = 1, int range = 1): base(characterManager, numTargets, range)
    {
        this.damage = damage;
    }

    protected override void ApplyToSelectedSpace(BoardSpace space)
    {
        int dealtDamage = space.Occupant.Damage(characterManager.ProfileManager.ModifiedValue(CharacterStatType.DAMAGE, damage));
        characterManager.Events.Attack.Process(new AttackEvent(space.Occupant, dealtDamage));
    }
}
