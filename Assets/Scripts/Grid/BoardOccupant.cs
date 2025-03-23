using System.Collections.Generic;
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

    public EventProcessor OnTurnBegin = new EventProcessor();

    public EventProcessor OnTurnEnd = new EventProcessor();

    public bool IsMoveable => MovementController != null;

    public abstract CharacterType GetCharacterType();

    public abstract ISet<StatusEffectType> Immunities { get; }

    public virtual TurnType? CharacterTurnType => GetCharacterType().GetDefaultTurnType();

    // Returns the amount of actual damage dealt
    public virtual int Damage(int damage, bool shieldable=false) {
        return 0;
    }

    public virtual void Heal(int healAmount) {
        
    }

    public virtual void ApplyStatus(StatusEffectType effectType) {

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

    protected virtual void Start()
    {
        if(!CharacterTurnType.HasValue) {
            return;
        }

        TurnMasterManager.Instance.OnTurnStarted.OnEvery(CheckForTurnBeginning);
        TurnMasterManager.Instance.OnTurnEnded.OnEvery(CheckForTurnEnding);
    }   

    private void CheckForTurnBeginning(TurnType turnType) {

        if(!CharacterTurnType.HasValue || turnType != CharacterTurnType.Value) {
            return;
        }
        
        OnTurnBegin.Process();
    }

    private void CheckForTurnEnding(TurnType turnType) {
        
        if(!CharacterTurnType.HasValue || turnType != CharacterTurnType.Value) {
            return;
        }
        
        OnTurnEnd.Process();
    }
}
