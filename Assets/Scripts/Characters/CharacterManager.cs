using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public abstract class CharacterManager : BoardOccupant
{
    [field:SerializeReference]
    public HealthController HealthController { get; private set; }

    [field:SerializeReference]
    public CharacterStatsManager StatsManager { get; private set; }

    [field:SerializeReference]
    public StatusEffectManager StatusEffectManager { get; private set; }

    public abstract ISelectionController GetAbilitySelectionController(AbilitySelectionCriteria abilitySelectionCriteria);

    public override ISet<StatusEffectType> Immunities => StatusEffectManager.CurrentImmunities;

    public override void Damage(int damage, bool shieldable=false)
    {
        HealthController?.TakeDamage(damage);
    }

    public override void ApplyStatus(StatusEffectType effectType)
    {
        StatusEffectManager.Apply(effectType);
    }
}
