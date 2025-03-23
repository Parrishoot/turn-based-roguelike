using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatPassive", menuName = "Abilities/Passive/Character Stat Adjust", order = 1)]
public class CharacterStatChangePassiveAbility : StatChangePassive<CharacterStatType>
{
    protected override StatChangePassiveController<CharacterStatType> GetStatPassiveController()
    {
        return new CharacterStatChangePassiveController(StatType, new ValueAdjuster(Amount, StatAdjustType));
    }
}
