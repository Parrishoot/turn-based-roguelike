using System;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private DeckManager deckManager;

    [SerializeField]
    private Transform handParentTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deckManager.OnCardDealt += SpawnCard;
    }

    private void SpawnCard(Ability ability)
    {
        AbilityCardController cardController = Instantiate(cardPrefab, handParentTransform).GetComponent<AbilityCardController>();
        cardController.Setup(ability);
    }
}
