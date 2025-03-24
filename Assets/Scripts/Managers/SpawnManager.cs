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

    public void SpawnPlayerCharacter(PlayerClass playerClass, BoardSpace space) {
        
        if(space.IsOccupied) {
            Debug.LogWarning("Trying to spawn character on occupied space");
            return;
        }

        spawner.SpawnPlayerCharacter(playerClass, (spawnedObject) => {
            PlayerCharacterManager playerCharacterManager = spawnedObject.GetComponent<PlayerCharacterManager>();
            space.Occupant = playerCharacterManager;

            PlayerCharacterSpawned.Process(playerCharacterManager);
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

            EnemyCharacterSpawned.Process(enemyCharacterManager);
        });
    }

    [ProButton]
    private void Test(EnemyClass enemyClass, Vector2Int space) {
        SpawnEnemy(enemyClass, BoardManager.Instance.Board[space.x, space.y]);
    }
}
