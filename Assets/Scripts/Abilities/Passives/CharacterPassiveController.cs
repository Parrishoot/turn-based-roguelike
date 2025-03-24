using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterPassiveController : CharacterPassiveControllerBase<CharacterManager>
{
    protected override List<CharacterManager> GetSpawnedCharacters()
    {
        return SpawnManager.Instance.AllCharacters;
    }

    protected override EventProcessor<CharacterManager> GetSpawnedEventProcessor()
    {
        return SpawnManager.Instance.CharacterSpawned;
    }
}
