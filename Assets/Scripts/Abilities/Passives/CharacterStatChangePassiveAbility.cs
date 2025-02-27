using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatPassive", menuName = "Abilities/Passive/Character Stat Adjust", order = 1)]
public class CharacterStatChangePassiveAbility : PassiveAbility
{
    [field:SerializeReference]
    public CharacterStatType StatType { get; private set; }

    [field:SerializeReference]
    public StatAdjuster.Type StatAdjustType { get; private set; } = StatAdjuster.Type.ADD;

    [field:SerializeReference]
    public int Amount { get; private set; }

    public override PassiveController GetController()
    {
        return new CharacterStatChangePassiveController(StatType, new StatAdjuster(Amount, StatAdjustType), Cost);
    }
}
