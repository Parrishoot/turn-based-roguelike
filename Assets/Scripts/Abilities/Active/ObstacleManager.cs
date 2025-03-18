using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleManager : BoardOccupant
{
    public override ISet<StatusEffectType> Immunities { 
        get {
            return Enum.GetValues(typeof(StatusEffectType))
                       .Cast<StatusEffectType>()
                       .ToHashSet();
        }
    }

    public override CharacterType GetCharacterType() => CharacterType.OBSTACLE;
}
