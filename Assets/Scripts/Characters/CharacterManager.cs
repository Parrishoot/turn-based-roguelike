using Pathfinding;
using UnityEngine;

public abstract class CharacterManager : BoardOccupant
{
    [field:SerializeReference]
    public MovementController MovementController { get; private set; }

    public abstract SelectionController GetSelectionController();

    public abstract CharacterType GetCharacterType(); 
}
