using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "CharacterStats", order = 1)]
public class CharacterStats : ScriptableObject
{
    [field:SerializeReference]
    public int Health = 5;
    
    [field:SerializeReference]
    public int Shield = 0;

    [field:SerializeReference]
    public int Movement = 2;

    [field:SerializeReference]
    public int Damage = 2;

    [field:SerializeReference]
    public int Range = 1;

}
