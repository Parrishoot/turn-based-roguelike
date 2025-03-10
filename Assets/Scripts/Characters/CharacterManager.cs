using Pathfinding;
using UnityEngine;

public abstract class CharacterManager : BoardOccupant
{
    [field:SerializeReference]
    public HealthController HealthController { get; private set; }

    [field:SerializeReference]
    public CharacterStatsManager StatsManager { get; private set; }

    public abstract ISelectionController GetAbilitySelectionController(AbilitySelectionCriteria abilitySelectionCriteria);

    public override void Damage(int damage)
    {
        HealthController?.TakeDamage(damage);
    }
}
