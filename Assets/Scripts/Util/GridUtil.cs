using Unity.VisualScripting;
using UnityEngine;

public static class GridUtil
{
    public static Vector3 GetCellCenterWorld(this Grid grid, int x, int y) {
        return grid.GetCellCenterWorld(new Vector2Int(x, y).AsXZ());
    }
}
