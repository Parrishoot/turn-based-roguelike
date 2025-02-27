using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public class MovementController : MonoBehaviour
{    
    public EventProcessor OnMovementFinished { get; private set; } = new EventProcessor(); 

    [SerializeField]
    private float movementSpeed = .5f;

    [field:SerializeReference]
    public CharacterManager CharacterManager { get; set; }

    private Queue<BoardSpace> currentPath = null;

    private BoardSpace targetSpace = null;

    public void MoveAlongPath(Path path) {

        if(path.Spaces.Count <= 0 || !path.IsValid) {
            Debug.LogWarning("Trying to move on an invalid path");
            return;
        }

        // If the starting node is our current space - just 
        // pop if off the queue
        BoardSpace startingSpace = path.Spaces.Peek();
        if(startingSpace.Cell == CharacterManager.Space.Cell) {
            path.Spaces.Dequeue();
        }

        currentPath = path.Spaces;
        ProcessNextMovment();

    }


    public void MoveToSpace(BoardSpace space) {
        PathNode pathNode= new PathNode(space);
        Path path = new Path(new List<PathNode>() { pathNode });

        MoveAlongPath(path);
    }

    private void ProcessNextMovment() {
        
        if(currentPath == null) {
            return;
        }

        if(currentPath.Count <= 0) {
            ProcessMovementEnded();
            return;
        }

        targetSpace = currentPath.Dequeue();
        transform.DOMove(targetSpace.WorldPosition, movementSpeed)
            .SetEase(Ease.InOutSine)
            .OnComplete(ProcessNextMovment);
        
    }

    private void ProcessMovementEnded() {

        targetSpace.Occupant = CharacterManager;
        
        targetSpace = null;
        currentPath = null;

        OnMovementFinished.Process();
    }
}
