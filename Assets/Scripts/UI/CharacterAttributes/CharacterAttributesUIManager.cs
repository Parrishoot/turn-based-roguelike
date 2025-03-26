using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAttributeUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TMP_Text characterNameText;

    [SerializeField]
    private HealthbarUIController healthbarUIController;

    [SerializeField]
    private Transform attributesPanelTransform;

    [Header("Prefabs")]    

    [SerializeField]
    private GameObject statsAttributeControllerPrefab;

    [SerializeField]
    private GameObject immunityControllerPrefab;

    [SerializeField]
    private GameObject statusEffectControllerPrefab;

    public CharacterManager CharacterManager { get; private set; }

    private List<CharacterAttributesUIController> attributeControllers = new List<CharacterAttributesUIController>();

    public void Init(CharacterManager characterManager) {
        
        CharacterManager = characterManager;
        CharacterManager.Events.Death.OnNext(() => {
            Destroy(gameObject);
        });

        characterNameText.text = characterManager.ProfileManager.Profile.CharacterName;
        healthbarUIController.Init(characterManager);

        InitPanels();
    }

    private void InitPanels()
    {
        AddStatsPanel();

        foreach(StatusEffectType statusEffectType in CharacterManager.ProfileManager.Profile.Stats.Immunities) {
            AddImmunityPanel(statusEffectType);
        }
    }

    public CharacterStatsUIController AddStatsPanel() {
        GameObject controllerObject = Instantiate(statsAttributeControllerPrefab, attributesPanelTransform);
        CharacterStatsUIController controller = controllerObject.GetComponent<CharacterStatsUIController>();
        controller.Init(CharacterManager);

        AddAttributeController(controller);

        return controller;
    }

    public ImmunityAttributeUIController AddImmunityPanel(StatusEffectType statusEffectType) {
        GameObject controllerObject = Instantiate(immunityControllerPrefab, attributesPanelTransform);
        ImmunityAttributeUIController controller = controllerObject.GetComponent<ImmunityAttributeUIController>();
        controller.Init(statusEffectType);

        AddAttributeController(controller);

        return controller;
    }

    public StatusEffectAttributeUIController AddStatusEffectPanel(StatusEffectController statusEffectController) {
        GameObject controllerObject = Instantiate(statusEffectControllerPrefab, attributesPanelTransform);
        StatusEffectAttributeUIController controller = controllerObject.GetComponent<StatusEffectAttributeUIController>();
        controller.Init(statusEffectController);

        AddAttributeController(controller);

        return controller;
    }

    public CharacterAttributesUIController AddAttributeController(CharacterAttributesUIController controller) {
        
        controller.gameObject.transform.SetParent(attributesPanelTransform, false);
        controller.Hide();

        attributeControllers.Add(controller);

        return controller;
    }

    public void RemoveAttributeController(CharacterAttributesUIController controller) {
        attributeControllers.Remove(controller);
        Destroy(controller.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach(CharacterAttributesUIController controller in attributeControllers) {
            controller.Show();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach(CharacterAttributesUIController controller in attributeControllers) {
            controller.Hide();
        }
    }
}
