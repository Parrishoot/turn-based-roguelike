using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTurnManager : TurnManager
{
    public override TurnType GetTurnType() => TurnType.ENEMY_CPU;

    public override void StartTurn()
    {
        if(SpawnManager.Instance.EnemyCharacters.Count == 0) {
            EndTurn();
            return;
        }

        // TODO: Actual logic
        NPCCharacterManager enemyNPC = SpawnManager.Instance.EnemyCharacters
            .Shuffled()
            .First();

        enemyNPC.AbilityManager.OnAbilityFinish.OnNext(EndTurn);
        enemyNPC.AbilityManager.UseAbility();
    }
}
