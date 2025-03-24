using UnityEngine;

public class EnemyCharacterManager : NPCCharacterManager
{
    [field:SerializeReference]
    public EnemyClass Class { get; private set; }

    public override CharacterType GetCharacterType() => CharacterType.ENEMY;
}
