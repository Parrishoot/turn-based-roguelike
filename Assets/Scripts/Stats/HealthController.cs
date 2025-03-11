using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager;

    public int CurrentHealth { 
        get {
            return Math.Max(0, MaxHealth - currentDamage);
        }
    }

    public int CurrentShield { 
        get {
            return characterManager.StatsManager.Stats[CharacterStatType.SHIELD].CurrentValue;
        }
    }

    public int MaxHealth { 
        get {
            return characterManager.StatsManager.Stats[CharacterStatType.HEALTH].CurrentValue;
        }
    }

    private int currentDamage;

    public Action<int> OnDamageTaken { get; set; }

    public Action<int> OnDamageShielded { get; set; }

    public Action<int> OnHeal { get; set; } 

    public Action OnDeath { get; set; }

    public void TakeDamage(int damage) {

        int remainingDamage = ProcessShield(damage);
        if(remainingDamage <= 0) {
            return;
        }

        currentDamage += remainingDamage;
        OnDamageTaken?.Invoke(remainingDamage);

        Debug.Log(gameObject.name + " took " + remainingDamage.ToString() + " damage!");

        if(CurrentHealth <= 0) {
            OnDeath?.Invoke();
        }
        
    }

    public void Heal(int health) {
        currentDamage = Math.Max(0, currentDamage - health);
        OnHeal?.Invoke(health);
    }

    private int ProcessShield(int damage)
    {
        int shieldedDamage = Mathf.Min(damage, CurrentShield);

        if(shieldedDamage > 0) {
            damage -= shieldedDamage;
            OnDamageShielded?.Invoke(shieldedDamage);
        }

        return damage;
    }
}
