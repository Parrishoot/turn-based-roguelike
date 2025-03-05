using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card", order = 1)]
public class Card : ScriptableObject
{
    [field:SerializeReference]
    public string CardName { get ; private set;}

    [field:SerializeReference]
    public int BaseCost { get; private set; } = 4;
    
    [field:SerializeReference]
    public ActiveAbility Active { get; private set; }

    [field:SerializeReference]
    public PassiveAbility Passive { get; private set; }
}
