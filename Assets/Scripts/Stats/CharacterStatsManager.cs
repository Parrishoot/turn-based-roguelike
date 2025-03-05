using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatsManager : StatsManager<CharacterStatType>
{
    [SerializeField]
    private CharacterStats characterStats;

    protected override Dictionary<CharacterStatType, AdjustableStat> InitStats()
    {
        Dictionary<CharacterStatType, AdjustableStat> statsDict = new Dictionary<CharacterStatType, AdjustableStat>();
        statsDict[CharacterStatType.HEALTH] = new AdjustableStat(characterStats.Health);
        statsDict[CharacterStatType.SHIELD] = new AdjustableStat(characterStats.Shield);
        statsDict[CharacterStatType.RANGE] = new AdjustableStat(characterStats.Range);
        statsDict[CharacterStatType.DAMAGE] = new AdjustableStat(characterStats.Damage);
        statsDict[CharacterStatType.MOVEMENT] = new AdjustableStat(characterStats.Movement);

        return statsDict;
    }
}
