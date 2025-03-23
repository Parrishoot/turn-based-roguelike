using UnityEngine;

/// <summary>
/// Removes a character's ability to heal
/// </summary>
public class CurseController : StatusEffectController
{
    private ValueAdjuster valueAdjuster;

    public CurseController(CharacterManager characterManager) : base(characterManager)
    {
        valueAdjuster = new ValueAdjuster(0, ValueAdjuster.Type.MULT);
    }

    public override StatusEffectType EffectType => StatusEffectType.CURSE;

    public override bool Perpetual => true;

    public override void Apply()
    {
        CharacterManager.HealthController.HealModifier.AddAdjuster(valueAdjuster);
    }

    public override void Remove()
    {
        CharacterManager.HealthController.HealModifier.RemoveAdjuster(valueAdjuster);
    }
}
