using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager;

    private List<StatusEffectController> statusEffects = new List<StatusEffectController>();

    public ISet<StatusEffectType> CurrentImmunities = new HashSet<StatusEffectType>();

    private void Start()
    {
        characterManager.OnTurnBegin.OnEvery(ProcessStatusExpiration);

        foreach(StatusEffectType effectType in characterManager.StatsManager.Immunities) {
            CurrentImmunities.Add(effectType);
        }
    }

    private void ProcessStatusExpiration()
    {
        for(int i = statusEffects.Count - 1; i >= 0; i--) {
            
            StatusEffectController controller = statusEffects[i];
            
            if(!controller.Perpetual) {
                controller.Remove();
                statusEffects.RemoveAt(i);
            }
        }
    }

    public void Apply(StatusEffectType statusEffectType) {

        // Return if Immune
        if(CurrentImmunities.Contains(statusEffectType)) {
            return;
        }

        //Return if there already exists a stat effect type of this
        // non stackable effect
        if(statusEffects.Where(x => x.EffectType == statusEffectType && !x.Stackable).Any()) {
            return;
        }

        StatusEffectController controller = statusEffectType.GetController(characterManager);
        controller.Apply();
        statusEffects.Add(controller);
    }
}
