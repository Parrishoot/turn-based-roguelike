using System;
using System.Collections.Generic;
using System.Linq;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    [SerializeField]
    private List<CardSO> cards;

    public EventProcessor<Card> OnCardDealt { get; set; } = new EventProcessor<Card>();

    public EventProcessor<Card> OnCardDiscarded { get; set; } = new EventProcessor<Card>();

    public Action OnDeckReshuffle { get; set; }

    public Queue<Card> CurrentDeck { get; private set; } 

    public List<Card> Graveyard { get; private set; }

    public List<Card> CurrentHand { get; private set; }

    private AdjustableStat handLimit;

    void Start()
    {
        handLimit = PlayerManager.Instance.StatsManager.Stats[PlayerStatType.HAND_SIZE];
        InitDeck();
    }

    private void InitDeck()
    {
        Graveyard = new List<Card>();
        CurrentDeck = new Queue<Card>(cards.Select(x => x.GetCard()).ToList().Shuffled());
        CurrentHand = new List<Card>();

        for(int i = 0; i < handLimit.CurrentValue; i++) {
            DrawCard();
        }
    }

    [ProButton]
    private void DrawCard()
    {
        if(CurrentHand.Count >= handLimit.CurrentValue || CurrentDeck.Count <= 0) {
            return;
        }

        Card dealtAbility = CurrentDeck.Dequeue();

        CurrentHand.Add(dealtAbility);
        OnCardDealt.Process(dealtAbility);
    }

    [ProButton]
    public void ReshuffleDeck() {

        List<Card> newDeckAbilities = CurrentDeck.ToList();
        newDeckAbilities.AddRange(Graveyard);

        Graveyard = new List<Card>();
        CurrentDeck = new Queue<Card>(newDeckAbilities.Shuffled());
        
        OnDeckReshuffle?.Invoke();
    }

    public void DiscardAbility(Card card)
    {
        Graveyard.Add(card);
        CurrentHand.Remove(card);

        OnCardDiscarded.Process(card);
    }

    public void DiscardDown()
    {
        while(CurrentHand.Count > handLimit.CurrentValue) {
            DiscardAbility(CurrentHand.Last());
        }
    }
}
