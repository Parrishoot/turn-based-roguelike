using Pathfinding;
using UnityEngine;

public abstract class BoardOccupant: MonoBehaviour
{
    // TODO: Implement
    private BoardSpace space;

    public BoardSpace Space { 

        get {
            return space;
        } 
        set {

            if(space != null) {
                space.Selectable.OnHoverStart -= HoverStart;
                space.Selectable.OnHoverEnd -= HoverEnd;
            }

            space = value;
            space.Selectable.OnHoverStart += HoverStart;
            space.Selectable.OnHoverEnd += HoverEnd;
        }
    }

    [field:SerializeReference]
    public MovementController MovementController { get; private set; }

    public EventProcessor OnSpaceHoverStart = new EventProcessor();

    public EventProcessor OnSpaceHoverEnd = new EventProcessor();


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

    private void HoverStart () {
        OnSpaceHoverStart.Process();
    }

    private void HoverEnd () {
        OnSpaceHoverEnd.Process();
    }
}
