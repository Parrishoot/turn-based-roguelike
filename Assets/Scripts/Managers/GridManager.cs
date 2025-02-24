using System;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [field:SerializeReference]
    public Vector2Int GridBounds { get; private set; } = Vector2Int.one * 5;

    [SerializeField]
    private Grid grid;

    [Header("Debug")]

    [SerializeField]
    private bool showGridLinesOnGizmo = true;

    [SerializeField]
    private bool showCellCoordsOnGizmo = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2Int GetClosestCellViaScreen(Vector2 screenPos) {

        Vector2Int closestCell = Vector2Int.zero;
        float distanceToBeat = float.MaxValue;

        // TODO: Update this if we increase the grid size
        for(int i = 0; i < GridBounds.x; i++) {
            for(int j = 0; j < GridBounds.y; j++) {
                Vector2 cellScreenPos = Camera.main.WorldToScreenPoint(grid.GetCellCenterWorld(i, j));
                float distance = Vector2.Distance(screenPos, cellScreenPos); 
                if(distance < distanceToBeat) {
                    closestCell = new Vector2Int(i, j);
                    distanceToBeat = distance;
                }
            }    
        }

        return closestCell;
    }

    public Vector2Int GetClosestCellViaWorld(Vector3 worldPos) {

        Vector2Int closestCell = Vector2Int.zero;
        float distanceToBeat = float.MaxValue;

        for(int i = 0; i < GridBounds.x; i++) {
            for(int j = 0; j < GridBounds.y; j++) {
                Vector3 cellPos = grid.GetCellCenterWorld(i, j);
                float distance = Vector3.Distance(worldPos, cellPos); 
                if(distance < distanceToBeat) {
                    closestCell = new Vector2Int(i, j);
                    distanceToBeat = distance;
                }
            }    
        }

        return closestCell;
    }

    public Vector3 GetCellCenterWorld(int x, int y) {
        return grid.GetCellCenterWorld(new Vector3Int(x, 0, y));
    }

    #region DEBUG

    private void OnDrawGizmos() {
        
        if(showGridLinesOnGizmo) {
            DrawGrid();
        }

        if(showCellCoordsOnGizmo) {
            DrawCoords();
        }

    }

    private void DrawGrid() {
        Gizmos.color = Color.red;

        for(int i = 0; i <= GridBounds.x; i++) {
            Vector3 startingPos = transform.position + Vector3.right * i * grid.cellSize.x;
            Gizmos.DrawLine(startingPos, startingPos + Vector3.forward * GridBounds.y * grid.cellSize.z);
        }

        for(int j = 0; j <= GridBounds.y; j++) {
            Vector3 startingPos = transform.position + Vector3.forward * j * grid.cellSize.z;
            Gizmos.DrawLine(startingPos, startingPos + Vector3.right * GridBounds.x * grid.cellSize.z);
        }

        Gizmos.color = Color.white;
    }

    private void DrawCoords() {
        
        for(int i = 0; i < GridBounds.x; i++) {
            for(int j = 0; j < GridBounds.y; j++) {
                string label = string.Format("{0},{1}", i, j);
                // Vector3 pos = transform.position + new Vector3(i * grid.cellSize.x, 0, j * grid.cellSize.y);
                Vector3 pos = grid.GetCellCenterWorld(i, j);
                Handles.Label(pos, label, GuiStyleUtil.GetDefaultHandleStyle());
            }
        }
        
    }

    #endregion
}
