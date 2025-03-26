using UnityEngine;

public class PlayCardSocketController : MonoBehaviour
{
    [SerializeField]
    private GameObject playCardSocketPrefab;

    [SerializeField]
    private PlayerCharacterManager playerCharacterManager;

    void Start()
    {
        PlayCardSocket playCardSocket = Instantiate(playCardSocketPrefab, PlayCardSocketPanelManager.Instance.transform, true).GetComponent<PlayCardSocket>();
        playCardSocket.Init(playerCharacterManager);
    }
}
