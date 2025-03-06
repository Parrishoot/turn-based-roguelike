using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatChangePassiveController : StatChangePassiveController<CharacterStatType>
{
    
    private IEventHandler<CharacterManager> characterSpawnedHandler;

    public CharacterStatChangePassiveController(CharacterStatType statType, StatAdjuster statAdjuster): base(statType, statAdjuster)
    {

    }

    protected override void ProcessDeactivation()
    {
        SpawnManager.Instance.CharacterSpawned.UnsubscribeHandler(characterSpawnedHandler);
        
        base.ProcessDeactivation();
    }

    protected override void ProcessActivation()
    {
        characterSpawnedHandler = new EveryEventHandler<CharacterManager>((manager) => manager.StatsManager.Stats[statType].AddAdjuster(statAdjuster));
        SpawnManager.Instance.CharacterSpawned.SubscribeHandler(characterSpawnedHandler);

        base.ProcessActivation();
    }

    protected override List<StatsManager<CharacterStatType>> GetStatsManagers()
    {
        List<StatsManager<CharacterStatType>> statsManagers = new List<StatsManager<CharacterStatType>>();

         // TODO: Migrate this to a manager
        List<CharacterStatsManager> characters = GameObject.FindObjectsByType<CharacterManager>(FindObjectsSortMode.None)
            .ToList()
            .Where(c => c.GetCharacterType() == CharacterType.PLAYER)
            .Select(c => c.StatsManager)
            .ToList();

        statsManagers.AddRange(characters);

        return statsManagers;
    }
}
