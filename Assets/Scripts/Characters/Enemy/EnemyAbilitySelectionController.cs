using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;

public class EnemyAbilitySelectionController : ISelectionController
{

    private AbilitySelectionCriteria abilitySelectionCriteria;
    private CharacterManager characterManager;

    public EnemyAbilitySelectionController(CharacterManager characterManager, AbilitySelectionCriteria abilitySelectionCriteria)
    {
        this.abilitySelectionCriteria = abilitySelectionCriteria;
        this.characterManager = characterManager;
    }

    public void BeginSelection(SelectionProcessor selectionProcessor)
    {
        List<BoardSpace> selection = new List<BoardSpace>();

        switch(abilitySelectionCriteria.CurrentAbilityType) {

            case AbilityType.OFFENSIVE:
                selection = SelectForOffensiveOrSupportAbility(selectionProcessor.GetCriteria());
                break;

            case AbilityType.SUPPORT:
                selection = SelectForOffensiveOrSupportAbility(selectionProcessor.GetCriteria());
                break;

            case AbilityType.MOVEMENT:
                if(abilitySelectionCriteria.OverallAbilityType == AbilityType.SUPPORT) {
                    selection = SelectForMovement(selectionProcessor, x => x.IsOccupied && characterManager.GetCharacterType().GetAlliedCharacterTypes().Contains(x.Occupant.GetCharacterType()));
                }
                else {
                    selection = SelectForMovement(selectionProcessor, x => x.IsOccupied && characterManager.GetCharacterType().GetOpposingCharacterTypes().Contains(x.Occupant.GetCharacterType()));
                }
                break;
        }

        selectionProcessor.ProcessSelection(selection);
    }

    private List<BoardSpace> SelectForMovement(SelectionProcessor selectionProcessor, Predicate<BoardSpace> targetCharacterPredicate)
    {
        // Find all the allies/enemies, sorted by distance
        List<BoardSpace> applicableBoardSpaces = BoardManager.Instance.GetMatchingSpaces(targetCharacterPredicate)
            .OrderBy(x => x.DistanceTo(characterManager.Space))
            .ToList();

        Queue<BoardSpace> characterQueue = new Queue<BoardSpace>(applicableBoardSpaces);
        
        while(characterQueue.Count > 0) {

            // Get the next closest character. If we're already within range,
            // no further movement is necessary
            BoardSpace targetCharacterSpace = characterQueue.Dequeue();
            
            if(targetCharacterSpace.DistanceTo(characterManager.Space, true) <= abilitySelectionCriteria.RangeToTarget) {
                return new List<BoardSpace>();
            }

            // Otherwise, determine if it's possible to reach the character
            List<Path> paths = BoardManager.Instance.GetMatchingSpaces(x => 
                    x != characterManager.Space && 
                    x.DistanceTo(targetCharacterSpace, true) <= abilitySelectionCriteria.RangeToTarget)
                .Select(x => PathFinder.FindPath(BoardManager.Instance.Board, characterManager.Space, x))
                .Where(x => x.IsValid)
                .OrderBy(x => x.Spaces.Count)
                .ToList();

            // If it is possible, find the shortest path and move as far as you can
            if(paths.Count > 0) {
                return GetClosestSpaceOnPath(selectionProcessor, paths.First());
            }
        }

        // If there isn't a path to any character, stand still
        return new List<BoardSpace>();
    }

    private List<BoardSpace> GetClosestSpaceOnPath(SelectionProcessor selectionProcessor, Path path)
    {
        SelectionCriteria selectionCriteria = selectionProcessor.GetCriteria();

        foreach(BoardSpace space in path.Spaces.Reverse()) {
            if(selectionCriteria.Filter(space)) {
                return new List<BoardSpace> { space };
            }
        }

        // Isn't possible, but stand still if we hit this case
        return new List<BoardSpace>();
    }

    private List<BoardSpace> SelectForOffensiveOrSupportAbility(SelectionCriteria selectionCriteria)
    {
        // If there are no selections to be made, return nothing
        if(selectionCriteria.MaxSelections <= 0) {
            return new List<BoardSpace>();
        }

        // Find all valid targets
        List<BoardSpace> targetSpaces = BoardManager.Instance.GetMatchingSpaces(selectionCriteria.Filter)
            .OrderBy(x => characterManager.Space.DistanceTo(x, true))
            .ToList();

        // If there are no valid targets, do nothing
        if(targetSpaces.Count == 0) {
            return new List<BoardSpace>();
        }

        // Otherwise, attack as many targets as you can
        return targetSpaces.GetRange(0, selectionCriteria.MaxSelections);
    }
}
