using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : StatsManager<PlayerStatType>
{
    [SerializeField]
    private PlayerStats playerStats;

    protected override Dictionary<PlayerStatType, AdjustableStat> InitStats()
    {
        Dictionary<PlayerStatType, AdjustableStat> statsDict = new Dictionary<PlayerStatType, AdjustableStat>();
        statsDict[PlayerStatType.MAX_MANA] = new AdjustableStat(playerStats.Mana);
        statsDict[PlayerStatType.MANA_REGEN] = new AdjustableStat(playerStats.ManaRegen);
        statsDict[PlayerStatType.MANA_COST] = new AdjustableStat(playerStats.ManaCost);
        statsDict[PlayerStatType.HAND_SIZE] = new AdjustableStat(playerStats.HandSize);

        return statsDict;
    }
}
