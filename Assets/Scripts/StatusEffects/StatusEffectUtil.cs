public static class StatusEffectUtil
{
    public static StatusEffectController GetController(this StatusEffectType effectType, CharacterManager characterManager) {
        return effectType switch
        {
            StatusEffectType.SHOCKWAVE => new ShockwaveController(characterManager),
            StatusEffectType.BLEED => new BleedController(characterManager),
            StatusEffectType.BOOST => new BoostController(characterManager),
            StatusEffectType.SIPHON => new SiphonController(characterManager),
            StatusEffectType.CURSE => new CurseController(characterManager),
            _ => throw new System.NotImplementedException(),
        };
    }
}
