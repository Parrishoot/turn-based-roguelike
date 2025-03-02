using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsUIController : MonoBehaviour
{
    [SerializeField]
    private List<CharacterStatType> ignoreStats;

    [SerializeField]
    private GameObject characterStatUIPrefab;
    
    [SerializeField]
    private CharacterManager characterManager;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private MySelectable selectable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(CharacterStatType stat in characterManager.StatsManager.Stats.Keys) {

            if(ignoreStats.Contains(stat)) {
                continue;
            }

            StatUIController statUIController = Instantiate(characterStatUIPrefab, panel.transform).GetComponent<StatUIController>();
            statUIController.Setup(characterManager, stat);

        }

        panel.SetActive(false);

        characterManager.OnSpaceHoverStart.OnEvery(() => panel.SetActive(true));
        characterManager.OnSpaceHoverEnd.OnEvery(() => panel.SetActive(false));
    }
}
