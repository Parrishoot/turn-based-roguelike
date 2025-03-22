using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public CharacterStats(int health, int shield, int movement, int damage, int range, List<StatusEffectType> immunities)
    {
        Health = health;
        Shield = shield;
        Movement = movement;
        Damage = damage;
        Range = range;
        Immunities = immunities;
    }

    [field: SerializeField]
    public int Health { get; private set; } = 5;

    [field: SerializeField]
    public int Shield { get; private set; } = 0;

    [field: SerializeField]
    public int Movement { get; private set; } = 2;

    [field: SerializeField]
    public int Damage { get; private set; } = 2;

    [field: SerializeField]
    public int Range { get; private set; } = 1;

    [field: SerializeField]
    public List<StatusEffectType> Immunities { get; private set; } = new List<StatusEffectType>();

}
