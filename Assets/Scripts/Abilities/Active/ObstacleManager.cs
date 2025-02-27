using UnityEngine;

public class ObstacleManager : BoardOccupant
{
    public override CharacterType GetCharacterType() => CharacterType.OBSTACLE;
}
