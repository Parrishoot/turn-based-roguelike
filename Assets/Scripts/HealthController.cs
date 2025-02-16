using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 10;

    [SerializeField]
    private int startingShield = 0;

    public int CurrentHealth { get; private set; }

    public int CurrentShield { get; private set; }

    public Action<int> OnDamageTaken { get; set; }

    public Action<int> OnDamageShielded { get; set; }

    public Action<int> OnHeal { get; set; } 

    public Action OnDeath { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = startingHealth;
        CurrentShield = startingShield;
    }

    public void TakeDamage(int damage) {

        int remainingDamage = ProcessShield(damage);
        if(remainingDamage <= 0) {
            return;
        }

        CurrentHealth -= remainingDamage;
        OnDamageTaken?.Invoke(remainingDamage);

        if(CurrentHealth <= 0) {
            OnDeath?.Invoke();
        }
        
    }

    public void Heal(int health) {
        CurrentHealth = Math.Min(CurrentHealth + health, startingHealth);
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
