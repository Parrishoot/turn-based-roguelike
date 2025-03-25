using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PushSelectionProcessor : SelectionProcessor
{
    private int numTargets = 1;

    private int range = 3;

    private CharacterManager characterManager;

    private int remainingPushes = 0;

    private List<BoardSpace> characterDestinations = new List<BoardSpace>();

    public PushSelectionProcessor(CharacterManager characterManager, int numTargets, int range)
    {
        this.characterManager = characterManager;
        this.numTargets = numTargets;
        this.range = range;
    }

    public override SelectionCriteria GetCriteria()
    {
        return new SelectionCriteria()
            .WithMaxSelections(numTargets)
            .WithFilter(space => {
               return space.DistanceTo(characterManager.Space) <= 1 &&
                    space.IsOccupied &&
                    space.Occupant.IsMoveable;
            });
    }

    public override void ProcessSelection(List<BoardSpace> selectedSpaces)
    {

        List<BoardSpace> characterSpaces = new List<BoardSpace>();

        foreach(BoardSpace space in selectedSpaces) {

            Vector2Int direction = space.Cell - characterManager.Space.Cell;
            Vector2Int targetSpace = space.Cell;

            for(int i = 1; i <= range; i++) {

                Vector2Int newTarget = space.Cell + (direction * i);

                if(!BoardManager.Instance.IsOpen(newTarget.x, newTarget.y)) {
                    break;
                }

                targetSpace = newTarget;
            }

            List<BoardSpace> pathSpaces = new List<BoardSpace> {
                space,
                BoardManager.Instance.Board[targetSpace.x, targetSpace.y]
            };

            Path path = Path.Make(pathSpaces);

            remainingPushes++;
            space.Occupant.MovementController.OnMovementFinished.OnNext(() => PushFinished(space.Occupant));
            space.Occupant.Move(path);

            characterSpaces.Add(path.Destination);
        }

        if(remainingPushes == 0) {
            OnSelectionProcessed?.Invoke(characterSpaces);
        }

    }

    private void PushFinished(BoardOccupant boardOccupant) {
        
        remainingPushes--;
        characterDestinations.Add(boardOccupant.Space);
        
        if(remainingPushes <= 0) {
            OnSelectionProcessed?.Invoke(characterDestinations);
        }
    }
}
