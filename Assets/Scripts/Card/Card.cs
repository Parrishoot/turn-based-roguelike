using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Card
{
    public Card(string cardName, int baseCost, ActiveAbility active, PassiveAbility passive, List<PlayerClass> characterClasses)
    {
        CardName = cardName;
        BaseCost = baseCost;
        Active = active;
        Passive = passive;
        this.characterClasses = characterClasses;
    }

    public string CardName { get; private set; }

    public int BaseCost { get; private set; } = 4;

    public ActiveAbility Active { get; private set; }

    public PassiveAbility Passive { get; private set; }

    private List<PlayerClass> characterClasses = new List<PlayerClass>();

    public List<PlayerClass> ApplicableClasses
    {
        get
        {
            if (characterClasses.Count != 0)
            {
                return characterClasses;
            }

            List<PlayerClass> classes = Enum.GetValues(typeof(PlayerClass)).Cast<PlayerClass>().ToList();
            classes.Remove(PlayerClass.TOTEM);

            return classes;
        }
        private set
        {
            characterClasses = value;
        }
    }

    public bool CanPlayOnCharacter(PlayerClass characterClass)
    {
        return ApplicableClasses.Contains(characterClass);
    }

    public PlayerClass? GetSpawnPlayerClass()
    {
        if (ApplicableClasses.Count > 1 || 
            ApplicableClasses.Contains(PlayerClass.TOTEM))
        {
            return null;
        }

        return ApplicableClasses.First();
    }
}
