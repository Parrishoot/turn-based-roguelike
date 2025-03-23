using UnityEngine;

public class AttackEvent
{
    public AttackEvent(BoardOccupant target, int damage)
    {
        Target = target;
        Damage = damage;
    }

    public BoardOccupant Target { get; private set; }

    public int Damage { get; private set; }
}
