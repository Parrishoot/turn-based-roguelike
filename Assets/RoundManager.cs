using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : Singleton<RoundManager>
{
    public EventProcessor OnRoundStarted = new EventProcessor();

    public EventProcessor OnRoundWon = new EventProcessor();

    public EventProcessor OnRoundLost = new EventProcessor();

    // TODO: Convert this to an actual round initializer
    private const int MAX_ENEMIES = 3;

    public void Start()
    {
        TurnMasterManager.Instance.OnTurnEnded.OnEvery((x) => Progress());    
    }

    public void BeginRound() {

        int remainingEnemies = Random.Range(1, MAX_ENEMIES);
        while(remainingEnemies > 0) {

            BoardSpace space = BoardManager.Instance.RandomSpace();

            if(space.IsOccupied) {
                continue;
            }

            SpawnManager.Instance.SpawnEnemy(EnemyClass.ZEALOT, space);
            remainingEnemies--;
        }

        OnRoundStarted.Process();
        TurnMasterManager.Instance.Begin();
    }

    public void Progress() {

        if(RoundWon()) {
            OnRoundWon.Process();
            return;
        }

        if(RoundLost()) {
            OnRoundLost.Process();
            return;
        }

        TurnMasterManager.Instance.TransitionToNextTurn();
    }

    private bool RoundWon() {
        return SpawnManager.Instance.EnemyCharacters.Count <= 0;
    }

    private bool RoundLost() {
        return !SpawnManager.Instance.PlayerCharacters.Exists(x => x.Class == PlayerClass.TOTEM);
    }
}
