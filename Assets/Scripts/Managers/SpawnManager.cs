using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private Transform occupantTransform;

    public EventProcessor<CharacterManager> CharacterSpawned { get; private set; } = new EventProcessor<CharacterManager>();

    public EventProcessor<PlayerCharacterManager> PlayerCharacterSpawned { get; private set; } = new EventProcessor<PlayerCharacterManager>();

    public EventProcessor<EnemyCharacterManager> EnemyCharacterSpawned { get; private set; } = new EventProcessor<EnemyCharacterManager>();
    private CharacterAddressablePrefabSpawner spawner = new CharacterAddressablePrefabSpawner();

    public List<PlayerCharacterManager> PlayerCharacters { get; private set; } = new List<PlayerCharacterManager>();

    public List<EnemyCharacterManager> EnemyCharacters { get; private set; } = new List<EnemyCharacterManager>();

    void Start()
    {
        foreach(PlayerCharacterManager manager in GameObject.FindObjectsByType<PlayerCharacterManager>(FindObjectsSortMode.None)) {
            PlayerCharacters.Add(manager);
            manager.Events.Death.OnNext(() => {
                PlayerCharacters.Remove(manager);
            });
        }

        foreach(EnemyCharacterManager manager in GameObject.FindObjectsByType<EnemyCharacterManager>(FindObjectsSortMode.None)) {
            EnemyCharacters.Add(manager);
            manager.Events.Death.OnNext(() => {
                EnemyCharacters.Remove(manager);
            });
        }
    }

    public List<CharacterManager> AllCharacters {
        get {
            List<CharacterManager> characters = new List<CharacterManager>();
            characters.AddRange(PlayerCharacters);
            characters.AddRange(EnemyCharacters);

            return characters;
        }
    }

    public void SpawnPlayerCharacter(PlayerClass playerClass, BoardSpace space) {
        
        if(space.IsOccupied) {
            Debug.LogWarning("Trying to spawn character on occupied space");
            return;
        }

        spawner.SpawnPlayerCharacter(playerClass, (spawnedObject) => {
            PlayerCharacterManager playerCharacterManager = spawnedObject.GetComponent<PlayerCharacterManager>();
            space.Occupant = playerCharacterManager;

            PlayerCharacters.Add(playerCharacterManager);
            playerCharacterManager.Events.Death.OnNext(() => {
                PlayerCharacters.Remove(playerCharacterManager);
            });

            PlayerCharacterSpawned.Process(playerCharacterManager);
            CharacterSpawned.Process(playerCharacterManager);
        });
    }

    public void SpawnEnemy(EnemyClass enemyClass, BoardSpace space) {
        
        if(space.IsOccupied) {
            Debug.LogWarning("Trying to spawn character on occupied space");
            return;
        }

        spawner.SpawnEnemy(enemyClass, (spawnedObject) => {
            EnemyCharacterManager enemyCharacterManager = spawnedObject.GetComponent<EnemyCharacterManager>();
            space.Occupant = enemyCharacterManager;

            EnemyCharacters.Add(enemyCharacterManager);
            enemyCharacterManager.Events.Death.OnNext(() => {
                EnemyCharacters.Remove(enemyCharacterManager);
            });
            
            EnemyCharacterSpawned.Process(enemyCharacterManager);
            CharacterSpawned.Process(enemyCharacterManager);
        });
    }

    [ProButton]
    private void Test(EnemyClass enemyClass, Vector2Int space) {
        SpawnEnemy(enemyClass, BoardManager.Instance.Board[space.x, space.y]);
    }
}
