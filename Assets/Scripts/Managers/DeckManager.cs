using System;
using System.Collections.Generic;
using System.Linq;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    [SerializeField]
    private List<Card> cards;
    
    [SerializeField]
    private int handLimit = 5;

    public Action<Card> OnCardDealt { get; set; }

    public Action OnDeckReshuffle { get; set; }

    public Queue<Card> CurrentDeck { get; private set; } 

    public List<Card> Graveyard { get; private set; }

    public List<Card> CurrentHand { get; private set; }

    void Start()
    {
        InitDeck();   
    }

    private void InitDeck()
    {
        Graveyard = new List<Card>();
        CurrentDeck = new Queue<Card>(cards.Shuffled());
        CurrentHand = new List<Card>();

        for(int i = 0; i < handLimit; i++) {
            DrawCard();
        }
    }

    [ProButton]
    private void DrawCard()
    {
        if(CurrentHand.Count >= handLimit || CurrentDeck.Count <= 0) {
            return;
        }

        Card dealtAbility = CurrentDeck.Dequeue();

        CurrentHand.Add(dealtAbility);
        OnCardDealt?.Invoke(dealtAbility);
    }

    [ProButton]
    public void ReshuffleDeck() {

        List<Card> newDeckAbilities = CurrentDeck.ToList();
        newDeckAbilities.AddRange(Graveyard);

        Graveyard = new List<Card>();
        CurrentDeck = new Queue<Card>(newDeckAbilities.Shuffled());
        
        OnDeckReshuffle?.Invoke();
    }

    internal void DiscardAbility(Card card)
    {
        Graveyard.Add(card);
        CurrentHand.RemoveAll(x => x.GetInstanceID() == card.GetInstanceID());
    }
}
