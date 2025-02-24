using UnityEngine;

public class PlayerTurnManager : TurnManager
{
    public override TurnType GetTurnType() => TurnType.PLAYER;

    public override void StartTurn()
    {
        // TODO: Implement
    }
}
