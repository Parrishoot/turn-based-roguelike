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
    private GameObject statsAttributeControllerPrefab;

    [SerializeField]
    private Transform attributesPanelTransform;

    [SerializeField]
    private HealthbarUIController healthbarUIController;

    public CharacterManager CharacterManager { get; private set; }

    private List<CharacterAttributesUIController> attributeControllers = new List<CharacterAttributesUIController>();

    public void Init(CharacterManager characterManager) {
        
        CharacterManager = characterManager;

        characterNameText.text = characterManager.ProfileManager.Profile.CharacterName;
        healthbarUIController.Init(characterManager);

        AddAttributeController(statsAttributeControllerPrefab);
    }

    public void AddAttributeController(GameObject prefab) {
        
        GameObject controllerObject = Instantiate(prefab, attributesPanelTransform);
        CharacterAttributesUIController controller = controllerObject.GetComponent<CharacterAttributesUIController>();

        if(controller == null) {
            Debug.LogWarning("Trying to instantiate attributes controller prefab with no attributes component. Destroying.");
            Destroy(controllerObject);
        } 

        AddAttributeController(controller);
    }

    public CharacterAttributesUIController AddAttributeController(CharacterAttributesUIController controller) {
        
        controller.gameObject.transform.SetParent(attributesPanelTransform, false);
        controller.Init(CharacterManager);
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
