using System;
using UnityEngine;

public class BoardSpace
{
    public BoardSpace(Vector2Int cell, GridSelectable selectable) {
        
        this.Cell = cell;
        
        this.Selectable = selectable;
        selectable.Space = this;

        this.occupant = null;

    }

    public Vector3 WorldPosition {
        get {
            return BoardManager.Instance.Grid.GetCellCenterWorld(Cell.x, Cell.y);
        }
    }

    public Vector2Int Cell;

    public GridSelectable Selectable { get; set; }

    private BoardOccupant occupant = null;

    public BoardOccupant Occupant { 
        get {
            return occupant;
        } 
        set {
            
            if(value != null && value.Space != null) {
                value.Space.Occupant = null;
            }
            
            occupant = value;

            if(value != null) {
                value.transform.position = WorldPosition;
                value.Space = this;
            }
        }
    }

    public bool IsOccupied {
        get  { return Occupant != null; }
    }

    public int DistanceTo(BoardSpace boardSpace, bool oneDirection=false) {

        int xDist = Math.Abs(Cell.x - boardSpace.Cell.x);
        int yDist = Math.Abs(Cell.y - boardSpace.Cell.y);

        return oneDirection ? Mathf.Min(xDist, yDist) : xDist + yDist;

    }
}
