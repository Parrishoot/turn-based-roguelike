using System;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class HandUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private DeckManager deckManager;

    [SerializeField]
    private RectTransform handParentTransform;

    [SerializeField]
    private float padding;

    [SerializeField]
    private float cardEaseTime = .25f;
    
    [SerializeField]
    private RectTransform deckTransform;

    private List<CardUIController> handCards = new List<CardUIController>();

    private bool resetPositions = false;

    void Awake()
    {
        deckManager.OnCardDealt.OnEvery(SpawnCard);
        deckManager.OnCardDiscarded.OnEvery(DiscardCard);
    }

    private void DiscardCard(Card card)
    {
        handCards.RemoveAll(x => x.Card == card);
        resetPositions = true;
    }

    private void SpawnCard(Card card)
    {
        CardUIController cardController = Instantiate(cardPrefab, deckTransform.position, Quaternion.identity).GetComponent<CardUIController>();
        cardController.transform.SetParent(handParentTransform);

        cardController.Setup(card);

        handCards.Add(cardController);
        resetPositions = true;
    }

    private void Update()
    {
        if(resetPositions) {
            ResetHandPositions();
        }
    }

    [ProButton]
    private void ResetHandPositions()
    {
        // This is here because the rect transform doesn't initialize from
        // frame 1 for some reason
        if(handParentTransform.rect.width <= 0) {
            return;
        }

        float width = handParentTransform.rect.width - (padding / 2);
        float pos_offset = width / (handCards.Count + 1);

        Vector2 startingPos = new Vector2(
            handParentTransform.rect.min.x + (padding / 2),
            handParentTransform.rect.center.y
        );

        
        for(int i = 0; i < handCards.Count; i++) {
            CardUIController card = handCards[i];
            card.TweenToPos(startingPos + Vector2.right * pos_offset * (i + 1), cardEaseTime);
        }

        resetPositions = false;
    }
}
