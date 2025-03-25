using UnityEngine;

public class StatAdjustAbilityProcessor : ActiveAbilityProcessor
{
    private ValueAdjuster statAdjuster;

    private CharacterStatType statType;

    public StatAdjustAbilityProcessor(CharacterManager characterManager, CharacterStatType statType, ValueAdjuster statAdjuster) : base(characterManager)
    {
        this.statAdjuster = statAdjuster;
        this.statType = statType;
    }

    public override void Process()
    {
        CharacterManager.ProfileManager.Stats[statType].Modifier.AddAdjuster(statAdjuster);
        AffectedSpaces.Add(CharacterManager.Space);

        OnAbilityFinish?.Invoke();
    }
}
