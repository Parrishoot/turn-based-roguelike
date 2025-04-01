using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    [field:SerializeReference]
    public GridManager Grid { get; private set; }

    [SerializeField]
    private GameObject gridSelectablePrefab;

    [SerializeField]
    private Transform gridSpacesTransform;

    public BoardSpace[,] Board { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        InitBoard();
    }

    private void InitBoard()
    {
        Board = new BoardSpace[Grid.GridBounds.x, Grid.GridBounds.y];

        SetupSpaces();
        
        // Check if any board occupants are in the Scene
        foreach(BoardOccupant occupant in FindObjectsByType<BoardOccupant>(FindObjectsSortMode.None)) {
            Vector2Int cell = Grid.GetClosestCellViaWorld(occupant.transform.position);
            if(!IsOccupied(cell.x, cell.y)) {
                Board[cell.x, cell.y].Occupant = occupant;
            }
            else {
                Debug.LogWarning(string.Format("Destroying {0} as there was no available cell", occupant.name));
                Destroy(occupant.gameObject);
            }
        }
    }

    private void SetupSpaces()
    {
        ForEachCell((x, y) => {
            Vector3 pos = Grid.GetCellCenterWorld(x, y);
            GridSelectable selectable = Instantiate(gridSelectablePrefab, pos, Quaternion.identity).GetComponent<GridSelectable>();
            selectable.gameObject.transform.SetParent(gridSpacesTransform, true);
            Board[x, y] = new BoardSpace(new Vector2Int(x, y), selectable);
        });
    }

    public void ForEachSpace(Action<BoardSpace> spaceAction) {
        ForEachCell((x, y) => spaceAction?.Invoke(Board[x, y]));
    }

    public void ForEachCell(Action<int, int> cellAction) {
        for(int i = 0; i < Grid.GridBounds.x; i++) {
            for(int j = 0; j < Grid.GridBounds.y; j++) {
                cellAction?.Invoke(i, j);
            }   
        }
    }

    public bool IsOccupied(int x, int y) {
        return IsValid(x, y) && Board[x, y].Occupant != null;
    }

    public bool IsOpen(int x, int y) {
        return IsValid(x, y) && Board[x, y].Occupant == null;
    }

    public bool IsValid(int x, int y) {
        return x >= 0 && 
            x < Grid.GridBounds.x && 
            y >= 0 &&
            y < Grid.GridBounds.y;
    }

    public List<BoardSpace> GetMatchingSpaces(Predicate<BoardSpace> filter) {

        List<BoardSpace> matchingSpaces = new List<BoardSpace>();

        for(int i = 0; i < Grid.GridBounds.x; i++) {
            for(int j = 0; j < Grid.GridBounds.y; j++) {
                if(filter.Invoke(Board[i, j])) {
                    matchingSpaces.Add(Board[i,j]);
                }
            }   
        }

        return matchingSpaces;
    }

    public BoardSpace RandomSpace() {
        int xCoord = UnityEngine.Random.Range(0, Grid.GridBounds.x - 1);
        int yCoord = UnityEngine.Random.Range(0, Grid.GridBounds.y - 1);

        return Board[xCoord, yCoord];
    }
}
