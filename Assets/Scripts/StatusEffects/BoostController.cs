using UnityEngine;

/// <summary>
/// Doubles a Character's Movement Distance for 1 turn
/// </summary>
public class BoostController : StatusEffectController
{
    private ValueAdjuster statAdjuster;

    public BoostController(CharacterManager characterManager) : base(characterManager)
    {
        statAdjuster = new ValueAdjuster(2, ValueAdjuster.Type.MULT);
    }

    public override StatusEffectType EffectType => StatusEffectType.BOOST;

    public override bool Perpetual => false;

    public override bool Negative => false;

    public override bool Stackable => false;

    public override void Apply()
    {
        CharacterManager.ProfileManager.Stats[CharacterStatType.MOVEMENT].Modifier.AddAdjuster(statAdjuster);
    }

    public override void Remove()
    {
        CharacterManager.ProfileManager.Stats[CharacterStatType.MOVEMENT].Modifier.RemoveAdjuster(statAdjuster);
    }
}
