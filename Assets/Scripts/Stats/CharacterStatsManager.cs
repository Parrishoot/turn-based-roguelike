using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [SerializeField]
    private CharacterStats characterStats;

    public Dictionary<CharacterStatType, AdjustableStat> Stats { get; private set; }

    private void Awake()
    {
        Stats = new Dictionary<CharacterStatType, AdjustableStat>();
        Stats[CharacterStatType.HEALTH] = new AdjustableStat(characterStats.Health);
        Stats[CharacterStatType.SHIELD] = new AdjustableStat(characterStats.Shield);
        Stats[CharacterStatType.RANGE] = new AdjustableStat(characterStats.Range);
        Stats[CharacterStatType.DAMAGE] = new AdjustableStat(characterStats.Damage);
        Stats[CharacterStatType.MOVEMENT] = new AdjustableStat(characterStats.Movement);
    }

    public int ModifiedValue(CharacterStatType statType, int baseValue) {
        return baseValue + Stats[statType].CurrentValue;
    }
}
