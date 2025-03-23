using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatPassive", menuName = "Abilities/Passive/Player Stat Adjust", order = 1)]
public class PlayerStatChangePassiveAbility : StatChangePassive<PlayerStatType>
{
    protected override StatChangePassiveController<PlayerStatType> GetStatPassiveController()
    {
        return new PlayerStatChangePassiveController(StatType, new ValueAdjuster(Amount, StatAdjustType));
    }
}