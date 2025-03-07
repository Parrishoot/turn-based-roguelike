using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card", order = 1)]
public class CardSO : ScriptableObject
{
    [field:SerializeReference]
    public string CardName { get ; private set;}

    [field:SerializeReference]
    public int BaseCost { get; private set; } = 4;
    
    [field:SerializeReference]
    public ActiveAbility Active { get; private set; }

    [field:SerializeReference]
    public PassiveAbility Passive { get; private set; }

    [field:SerializeReference]
    public List<CharacterClass> CharacterClasses { get; private set; }

    public Card GetCard() {
        return new Card(CardName, BaseCost, Active, Passive, CharacterClasses);
    }
}
