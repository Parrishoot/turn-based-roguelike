using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public abstract class CharacterManager : BoardOccupant
{
    [field:SerializeReference]
    public CharacterProfileManager ProfileManager { get; private set; }

    [field:SerializeReference]
    public StatusEffectManager StatusEffectManager { get; private set; }

    public abstract ISelectionController GetAbilitySelectionController(AbilitySelectionCriteria abilitySelectionCriteria);

    public CharacterEvents Events { get; } = new CharacterEvents();

    public override ISet<StatusEffectType> Immunities => StatusEffectManager.CurrentImmunities;

    protected override void Start() {
        
        base.Start();
        
        CharacterPanelManager.Instance.AddCharacterAttributePanel(this);

        if(HealthController != null) {
            HealthController.OnDeath += Events.Death.Process;
        }
    }

    public override void ApplyStatus(StatusEffectType effectType)
    {
        StatusEffectManager.Apply(effectType);
    }

    public void RemoveStatus(StatusEffectType effectType)
    {
        StatusEffectManager.Remove(effectType);
    }
}
