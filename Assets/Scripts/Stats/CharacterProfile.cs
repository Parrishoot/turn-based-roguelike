using UnityEngine;

[CreateAssetMenu(fileName = "Profile", menuName = "Profile/CharacterProfile", order = 1)]
public class CharacterProfile : ScriptableObject
{
    [field:SerializeField]
    public string CharacterName { get; private set; }

    [field: SerializeField]
    public CharacterStats Stats { get; private set; }
}
