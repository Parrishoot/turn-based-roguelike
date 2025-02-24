using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class TurnMasterManager: Singleton<TurnMasterManager>
{
    [SerializeField]
    private List<TurnManager> turnManagers = new List<TurnManager>();

    public EventProcessor<TurnType> OnTurnStarted = new EventProcessor<TurnType>();

    private LoopingQueue<TurnManager> turnQueue;

    private void Start() {

        turnQueue = new LoopingQueue<TurnManager>(turnManagers);

        foreach(TurnManager turnManager in turnManagers) {
            turnManager.OnTurnFinished.OnEvery(TransitionToNextTurn);
        } 
    }

    private void TransitionToNextTurn() {
        
        TurnManager next = turnQueue.GetNext();

        OnTurnStarted.Process(next.GetTurnType());
        next.StartTurn();
    }

    [ProButton]
    public void Begin() {
        TransitionToNextTurn();
    }
}
