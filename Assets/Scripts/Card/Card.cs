using UnityEngine;

public class Card
{
    public Card(string cardName, int baseCost, ActiveAbility active, PassiveAbility passive)
    {
        CardName = cardName;
        BaseCost = baseCost;
        Active = active;
        Passive = passive;
    }

    public string CardName { get ; private set;}

    public int BaseCost { get; private set; } = 4;
    
    public ActiveAbility Active { get; private set; }

    public PassiveAbility Passive { get; private set; }
}
