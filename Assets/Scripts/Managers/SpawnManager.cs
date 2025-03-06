using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private GameObject playerCharacterPrefab;

    [SerializeField]
    private Transform occupantTransform;

    public EventProcessor<CharacterManager> CharacterSpawned { get; private set; } = new EventProcessor<CharacterManager>();

    public void SpawnAtSpace(BoardSpace space) {

        if(space.IsOccupied) {
            Debug.LogWarning("Trying to spawn character on occupied space");
            return;
        }

        CharacterManager occupant = Instantiate(playerCharacterPrefab, occupantTransform).GetComponent<CharacterManager>();
        space.Occupant = occupant;

        CharacterSpawned.Process(occupant);
    }
}
