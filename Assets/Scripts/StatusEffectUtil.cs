public static class StatusEffectUtil
{
    public static StatusEffectController GetController(this StatusEffectType effectType, CharacterManager characterManager, bool perpetual=true) {
        return effectType switch
        {
            StatusEffectType.SHOCKWAVE => new ShockwaveController(characterManager, perpetual),
            _ => throw new System.NotImplementedException(),
        };
    }
}
