using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class TurnMasterManager: Singleton<TurnMasterManager>
{
    [SerializeField]
    private List<TurnManager> turnManagers = new List<TurnManager>();

    public EventProcessor<TurnType> OnTurnStarted = new EventProcessor<TurnType>();

    public EventProcessor<TurnType> OnTurnEnded = new EventProcessor<TurnType>();

    private LoopingQueue<TurnManager> turnQueue;

    private TurnManager currentTurnManager;

    private void Start() {

        turnQueue = new LoopingQueue<TurnManager>(turnManagers);

        foreach(TurnManager turnManager in turnManagers) {
            turnManager.OnTurnFinished.OnEvery(TransitionToNextTurn);
        } 
    }

    private void TransitionToNextTurn() {

        if(currentTurnManager != null) {
            OnTurnEnded.Process(currentTurnManager.GetTurnType());
        }   

        currentTurnManager = turnQueue.GetNext();

        OnTurnStarted.Process(currentTurnManager.GetTurnType());
        currentTurnManager.StartTurn();
    }

    [ProButton]
    public void Begin() {
        TransitionToNextTurn();
    }
}
