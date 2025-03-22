using UnityEngine;

public class StatAdjustAbilityProcessor : ActiveAbilityProcessor
{
    private StatAdjuster statAdjuster;

    private CharacterStatType statType;

    public StatAdjustAbilityProcessor(CharacterManager characterManager, CharacterStatType statType, StatAdjuster statAdjuster) : base(characterManager)
    {
        this.statAdjuster = statAdjuster;
        this.statType = statType;
    }

    public override void Process()
    {
        CharacterManager.ProfileManager.Stats[statType].AddAdjuster(statAdjuster);
        OnAbilityFinish?.Invoke();
    }
}
