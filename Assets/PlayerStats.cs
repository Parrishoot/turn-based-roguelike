using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [field:SerializeReference]
    public int Mana = 20;
    
    [field:SerializeReference]
    public int ManaRegen = 5;

    [field:SerializeReference]
    public int ManaCost = 0;

    [field:SerializeReference]
    public int HandSize = 5;
}
