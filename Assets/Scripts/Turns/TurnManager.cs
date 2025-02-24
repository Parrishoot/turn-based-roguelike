using UnityEngine;

public abstract class TurnManager: MonoBehaviour
{
    public EventProcessor OnTurnFinished = new EventProcessor();

    public abstract void StartTurn();

    public abstract TurnType GetTurnType();

    public void EndTurn() {
        OnTurnFinished.Process();
    }
}
