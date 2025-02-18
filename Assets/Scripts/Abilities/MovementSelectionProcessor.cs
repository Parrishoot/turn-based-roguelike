using System.Collections.Generic;
using System.Linq;
using Pathfinding;

public class MovementSelectionProcessor : SelectionProcessor
{
    private CharacterManager characterManager;

    private int range = 1;

    public MovementSelectionProcessor(CharacterManager characterManager, int range = 1) {
        this.characterManager = characterManager;
        this.range = range;
    }

    public override SelectionCriteria GetCriteria()
    {
        return new SelectionCriteria()
            .WithFilter(IsWalkable);

    }

    protected bool IsWalkable(BoardSpace boardSpace) {
        
        // Check for basic distance and occupation of space
        int distance = characterManager.Space.DistanceTo(boardSpace);
        if(boardSpace.IsOccupied || distance > range) {
            return false;
        }

        // Check for available path
        Path path = PathFinder.FindPath(BoardManager.Instance.Board, characterManager.Space, boardSpace, range);
        return path.IsValid;
    }

    public override void ProcessSelection(List<BoardSpace> selectedSpaces)
    {
        if(selectedSpaces.Count <= 0) {
            OnSelectionProcessed?.Invoke();
            return;
        }

        BoardSpace targetSpace = selectedSpaces.First();
        Path path = PathFinder.FindPath(BoardManager.Instance.Board, characterManager.Space, targetSpace, range);

        characterManager.MovementController.OnNextMovementFinished += () => OnSelectionProcessed?.Invoke();
        characterManager.MovementController.MoveAlongPath(path);
    }
}
