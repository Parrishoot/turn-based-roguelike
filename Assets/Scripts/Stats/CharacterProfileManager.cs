using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterProfileManager : StatsManager<CharacterStatType>
{
    [field:SerializeReference]
    public CharacterProfile Profile { get; private set; }

    public List<StatusEffectType> Immunities => Profile.Stats.Immunities;

    protected override Dictionary<CharacterStatType, AdjustableStat> InitStats()
    {
        Dictionary<CharacterStatType, AdjustableStat> statsDict = new Dictionary<CharacterStatType, AdjustableStat>();
        statsDict[CharacterStatType.HEALTH] = new AdjustableStat(Profile.Stats.Health);
        statsDict[CharacterStatType.SHIELD] = new AdjustableStat(Profile.Stats.Shield);
        statsDict[CharacterStatType.RANGE] = new AdjustableStat(Profile.Stats.Range);
        statsDict[CharacterStatType.DAMAGE] = new AdjustableStat(Profile.Stats.Damage);
        statsDict[CharacterStatType.MOVEMENT] = new AdjustableStat(Profile.Stats.Movement);

        return statsDict;
    }
}
