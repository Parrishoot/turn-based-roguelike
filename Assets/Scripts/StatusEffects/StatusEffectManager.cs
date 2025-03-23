using System;
using System.Collections.Generic;
using System.Linq;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager;

    private List<StatusEffectController> statusEffects = new List<StatusEffectController>();

    private List<StatusEffectAttributeUIController> uiPanels = new List<StatusEffectAttributeUIController>();

    public ISet<StatusEffectType> CurrentImmunities = new HashSet<StatusEffectType>();

    private void Start()
    {
        characterManager.OnTurnBegin.OnEvery(ProcessStatusExpiration);

        foreach(StatusEffectType effectType in characterManager.ProfileManager.Immunities) {
            CurrentImmunities.Add(effectType);
        }
    }

    private void ProcessStatusExpiration()
    {
        for(int i = statusEffects.Count - 1; i >= 0; i--) {
            
            StatusEffectController controller = statusEffects[i];
            
            if(!controller.Perpetual) {
                Remove(controller);
            }
        }
    }

    public void Remove(StatusEffectController statusEffectController) {
        statusEffectController.Remove();
        statusEffects.Remove(statusEffectController);
        RemoveUIPanel(statusEffectController);
    }

    [ProButton]
    public void Remove(StatusEffectType statusEffectType) {

        StatusEffectController statusEffectController = statusEffects.Where(x => x.EffectType == statusEffectType).First();

        if(statusEffectController != null) {
            Remove(statusEffectController);
        }
    }

    private void RemoveUIPanel(StatusEffectController statusEffectController) {
        
        StatusEffectAttributeUIController controller = uiPanels.Where(x => x.Controller == statusEffectController).First();
        CharacterAttributeUIManager attributeManager = CharacterPanelManager.Instance.GetAttributePanelForCharacter(characterManager);
        
        if(controller == null || attributeManager == null) {
            return;
        }

        attributeManager.RemoveAttributeController(controller);
        uiPanels.Remove(controller);
    }

    [ProButton]
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

        CharacterAttributeUIManager attributeManager = CharacterPanelManager.Instance.GetAttributePanelForCharacter(characterManager);
        if(attributeManager == null) {
            return;
        }

        uiPanels.Add(attributeManager.AddStatusEffectPanel(controller));
    }
}
