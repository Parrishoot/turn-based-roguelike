using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatChangePassiveController : StatChangePassiveController<CharacterStatType>
{
    
    private IEventHandler<PlayerCharacterManager> characterSpawnedHandler;
    private List<PlayerClass> applicableClasses;

    public CharacterStatChangePassiveController(CharacterStatType statType, ValueAdjuster statAdjuster, List<PlayerClass> applicableClasses) : base(statType, statAdjuster)
    {
        this.applicableClasses = applicableClasses;
    }

    protected override void ProcessDeactivation()
    {
        SpawnManager.Instance.PlayerCharacterSpawned.UnsubscribeHandler(characterSpawnedHandler);
        
        base.ProcessDeactivation();
    }

    protected override void ProcessActivation()
    {
        characterSpawnedHandler = new EveryEventHandler<PlayerCharacterManager>((manager) => {
            if (applicableClasses.Contains(manager.Class)) {
                manager.ProfileManager.Stats[statType].Modifier.AddAdjuster(statAdjuster);  
            }
        });

        SpawnManager.Instance.PlayerCharacterSpawned.SubscribeHandler(characterSpawnedHandler);

        base.ProcessActivation();
    }

    protected override List<StatsManager<CharacterStatType>> GetStatsManagers()
    {
        List<StatsManager<CharacterStatType>> statsManagers = new List<StatsManager<CharacterStatType>>();

        
        List<CharacterProfileManager> characters = SpawnManager.Instance.PlayerCharacters
            .ToList()
            .Where(c => applicableClasses.Contains(c.Class))
            .Select(c => c.ProfileManager)
            .ToList();

        statsManagers.AddRange(characters);

        return statsManagers;
    }
}
