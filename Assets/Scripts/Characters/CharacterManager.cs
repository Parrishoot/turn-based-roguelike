using Pathfinding;
using UnityEngine;

public abstract class CharacterManager : BoardOccupant
{
    [field:SerializeReference]
    public MovementController MovementController { get; private set; }

    [field:SerializeReference]
    public HealthController HealthController { get; private set; }

    public abstract ISelectionController GetSelectionController();

    public override void Damage(int damage)
    {
        HealthController?.TakeDamage(damage);
    }
}
