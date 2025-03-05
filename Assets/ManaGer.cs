using System;
using UnityEngine;

public class ManaGer : Singleton<ManaGer>
{
    [SerializeField]
    private PlayerStatsManager playerStatsManager;

    public void Start()
    {
        TurnMasterManager.Instance.OnTurnStarted.OnEvery(CheckRefreshMana);
    }

    private void CheckRefreshMana(TurnType type)
    {
        if(type != TurnType.PLAYER) {
            return;
        }

        BasicRefresh();
    }

    public int CurrentMana { 
        get {
            return Math.Max(0, playerStatsManager.Stats[PlayerStatType.MAX_MANA].CurrentValue - drainedMana);
        }
    }

    private int drainedMana = 0;

    public void BasicRefresh() {
        drainedMana = Mathf.Max(0, drainedMana - playerStatsManager.Stats[PlayerStatType.MANA_REGEN].CurrentValue);
    }

    public void SpendMana(int baseCost) {

        int cost = AdjustedCost(baseCost);

        if(cost > CurrentMana) {
            Debug.LogWarning("Trying to spend more mana than you have");
            return;
        }

        drainedMana += cost;
    }

    public int AdjustedCost(int baseCost) {
        return playerStatsManager.ModifiedValue(PlayerStatType.MANA_COST, baseCost);
    }

    public bool ManaAvailable(int baseCost) {
        int cost = AdjustedCost(baseCost);
        return cost <= CurrentMana;
    }
}
