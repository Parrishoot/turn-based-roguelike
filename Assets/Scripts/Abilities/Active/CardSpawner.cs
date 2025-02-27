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

    void Awake()
    {
        deckManager.OnCardDealt += SpawnCard;
    }

    private void SpawnCard(Card card)
    {
        CardUIController cardController = Instantiate(cardPrefab, handParentTransform).GetComponent<CardUIController>();
        cardController.Setup(card);
    }
}
