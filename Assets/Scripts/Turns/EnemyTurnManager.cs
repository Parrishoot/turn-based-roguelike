using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTurnManager : TurnManager
{
    public override TurnType GetTurnType() => TurnType.CPU;

    public override void StartTurn()
    {
        // TODO: Actual logic
        NPCCharacterManager enemyNPC = FindObjectsByType<NPCCharacterManager>(FindObjectsSortMode.None)
            .Where(x => x.GetCharacterType() == CharacterType.ENEMY)
            .ToList()
            .Shuffled()
            .First();

        enemyNPC.AbilityManager.OnAbilityFinish.OnNext(EndTurn);
        enemyNPC.AbilityManager.UseAbility();
    }
}
