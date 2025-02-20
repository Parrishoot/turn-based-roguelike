using System;
using System.Collections.Generic;
using System.Linq;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    [SerializeField]
    private List<Ability> abilities;
    
    [SerializeField]
    private int handLimit = 5;

    public Action<Ability> OnCardDealt { get; set; }

    public Action OnDeckReshuffle { get; set; }

    public Queue<Ability> CurrentDeck { get; private set; } 

    public List<Ability> Graveyard { get; private set; }

    public List<Ability> CurrentHand { get; private set; }

    void Start()
    {
        InitDeck();   
    }

    private void InitDeck()
    {
        Graveyard = new List<Ability>();
        CurrentDeck = new Queue<Ability>(abilities.Shuffled());
        CurrentHand = new List<Ability>();

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

        Ability dealtAbility = CurrentDeck.Dequeue();

        CurrentHand.Add(dealtAbility);
        OnCardDealt?.Invoke(dealtAbility);
    }

    [ProButton]
    public void ReshuffleDeck() {

        List<Ability> newDeckAbilities = CurrentDeck.ToList();
        newDeckAbilities.AddRange(Graveyard);

        Graveyard = new List<Ability>();
        CurrentDeck = new Queue<Ability>(newDeckAbilities.Shuffled());
        
        OnDeckReshuffle?.Invoke();
    }

    internal void DiscardAbility(Ability ability)
    {
        Graveyard.Add(ability);
        CurrentHand.RemoveAll(x => x.GetInstanceID() == ability.GetInstanceID());
    }
}
