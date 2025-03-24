using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacterPassiveController : CharacterPassiveControllerBase<PlayerCharacterManager>
{
    protected PlayerCharacterPassiveController(Predicate<PlayerCharacterManager> filter = null) : base(filter)
    {

    }

    protected override List<PlayerCharacterManager> GetSpawnedCharacters()
    {
        return SpawnManager.Instance.PlayerCharacters;
    }

    protected override EventProcessor<PlayerCharacterManager> GetSpawnedEventProcessor()
    {
        return SpawnManager.Instance.PlayerCharacterSpawned;
    }
}
