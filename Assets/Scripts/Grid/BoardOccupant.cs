using Pathfinding;
using UnityEngine;

public abstract class BoardOccupant: MonoBehaviour
{
    // TODO: Implement
    public BoardSpace Space { get; set; }

    [field:SerializeReference]
    public MovementController MovementController { get; private set; }

    public bool IsMoveable { 
        get {
            return MovementController != null;
        }
    }

    public abstract CharacterType GetCharacterType();

    public virtual void Damage(int damage) {

    }

    public virtual void Move(Path path) {

        if(!IsMoveable) {
            return;
        }

        MovementController.MoveAlongPath(path);
    }
}
