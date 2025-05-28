using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatPassive", menuName = "Abilities/Passive/Player Stat Adjust", order = 1)]
public class PlayerStatChangePassiveAbility : StatChangePassive<PlayerStatType>
{
    protected override StatChangePassiveController<PlayerStatType> GetStatPassiveController(List<PlayerClass> applicableClasses)
    {
        return new PlayerStatChangePassiveController(StatType, new ValueAdjuster(Amount, StatAdjustType));
    }
}