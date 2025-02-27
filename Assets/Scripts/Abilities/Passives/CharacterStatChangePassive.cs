using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatChangePassiveController : PassiveController
{
    private CharacterStatType statType;

    private StatAdjuster statAdjuster;

    public CharacterStatChangePassiveController(CharacterStatType statType, StatAdjuster statAdjuster, int cost): base(cost)
    {
        this.statType = statType;
        this.statAdjuster = statAdjuster;
    }

    protected virtual List<CharacterManager> GetCharacters() {
        // TODO: Migrate this to a manager
        return GameObject.FindObjectsByType<CharacterManager>(FindObjectsSortMode.None)
                .ToList()
                .Where(c => c.GetCharacterType() == CharacterType.PLAYER)
                .ToList();
    }

    protected override void ProcessDeactivation()
    {
        foreach(CharacterManager c in GetCharacters()) {
            c.StatsManager.Stats[statType].RemoveAdjuster(statAdjuster);
        }
    }

    protected override void ProcessActivation()
    {
        foreach(CharacterManager c in GetCharacters()) {
            c.StatsManager.Stats[statType].AddAdjuster(statAdjuster);
        }
    }
}
