using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CharacterAddressablePrefabSpawner
{
    private const string BASE_PATH = "Assets/Prefabs/Characters";

    private string PlayerCharacterPath {
        get {
            return string.Format("{0}/Player", BASE_PATH);
        }
    }

    private string EnemyCharacterPath {
        get {
            return string.Format("{0}/Enemy", BASE_PATH);
        }
    }

    private string GetAssetPathForPlayerCharacter(PlayerClass playerClass) {
        return string.Format("{0}/{1}.prefab", PlayerCharacterPath, playerClass);
    }

    private string GetAssetPathForEnemyCharacter(EnemyClass enemyClass) {
        return string.Format("{0}/{1}.prefab", EnemyCharacterPath, enemyClass);
    }

    public void SpawnPlayerCharacter(PlayerClass playerClass, Action<GameObject> onSpawn) {
        Addressables.LoadAssetAsync<GameObject>(GetAssetPathForPlayerCharacter(playerClass)).Completed += (handler) => Spawn(handler, onSpawn, playerClass);
    }

    public void SpawnEnemy(EnemyClass enemyClass, Action<GameObject> onSpawn) {
        Addressables.LoadAssetAsync<GameObject>(GetAssetPathForEnemyCharacter(enemyClass)).Completed += (handler) => Spawn(handler, onSpawn, enemyClass);
    }

    private void Spawn<T>(AsyncOperationHandle<GameObject> handler, Action<GameObject> onSpawn, T characterClass) {

        if(handler.Status == AsyncOperationStatus.Succeeded) {
            GameObject spawnedObject = GameObject.Instantiate(handler.Result);
            onSpawn?.Invoke(spawnedObject);
        }
        else {
            string reason = "Unknown";

            if(handler.OperationException != null) {
                reason = handler.OperationException.Message;
            }

            Debug.LogWarning(string.Format("Unable to load: {0} because of {1}", characterClass.ToString(), reason));
        }

    }
}
