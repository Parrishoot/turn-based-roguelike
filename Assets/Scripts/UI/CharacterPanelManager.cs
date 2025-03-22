using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterPanelManager : Singleton<CharacterPanelManager>
{
    [SerializeField]
    private GameObject characterAttributePanelPrefab;

    private List<CharacterAttributeUIManager> characterAttributePanels = new List<CharacterAttributeUIManager>();

    public void AddCharacterAttributePanel(CharacterManager characterManager) {
        
        CharacterAttributeUIManager attributeManager = Instantiate(characterAttributePanelPrefab, transform).GetComponent<CharacterAttributeUIManager>();
        attributeManager.Init(characterManager);

        characterAttributePanels.Add(attributeManager);
    }

    public void RemoveAttributeController(CharacterManager characterManager) {
        
        CharacterAttributeUIManager attributeManager = GetAttributePanelForCharacter(characterManager);
        
        if(attributeManager == null) {
            return;
        }
        
        characterAttributePanels.Remove(attributeManager);
        Destroy(attributeManager.gameObject);
    }

    public CharacterAttributeUIManager GetAttributePanelForCharacter(CharacterManager characterManager) {
        return characterAttributePanels.Where(x => x.CharacterManager == characterManager).First();
    }
}
