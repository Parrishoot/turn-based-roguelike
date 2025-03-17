using System;
using System.Linq;

/*
 *  Applies 1 damage to all adjacent allies on damage taken
 */ 
public class ShockwaveController : StatusEffectController
{
    private const int DAMAGE = 1;

    public ShockwaveController(CharacterManager characterManager, bool perpetual=true) : base(characterManager, perpetual)
    {
    }

    public override StatusEffectType EffectType => StatusEffectType.SHOCKWAVE;

    public override void Apply()
    {
        CharacterManager.HealthController.OnDamageTaken += ApplyDamage;
    }

    private void ApplyDamage(int damage)
    {
        foreach(BoardSpace adjacentSpace in CharacterManager.Space.AdjacentSpaces) {
            
            if(!CharacterManager.GetCharacterType().GetAlliedCharacterTypes().Contains(adjacentSpace.Occupant.GetCharacterType())) {
                return;
            }

            adjacentSpace.Occupant.Damage(DAMAGE, true);
        }
    }

    public override void Remove()
    {
        CharacterManager.HealthController.OnDamageTaken -= ApplyDamage;
    }
}
