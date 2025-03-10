using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public Card(string cardName, int baseCost, ActiveAbility active, PassiveAbility passive, List<CharacterClass> characterClasses)
    {
        CardName = cardName;
        BaseCost = baseCost;
        Active = active;
        Passive = passive;
        CharacterClasses = characterClasses;
    }

    public string CardName { get ; private set;}

    public int BaseCost { get; private set; } = 4;
    
    public ActiveAbility Active { get; private set; }

    public PassiveAbility Passive { get; private set; }

    public List<CharacterClass> CharacterClasses{ get; private set; }

    public bool CanPlayOnCharacter(CharacterClass characterClass) {

        // If none are supplied - assume all but Totem are allowed
        if(CharacterClasses.Count == 0) {
            return characterClass != CharacterClass.TOTEM;
        }

        return CharacterClasses.Contains(characterClass);
    }
}
