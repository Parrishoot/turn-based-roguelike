using UnityEngine;

public static class Vector2IntUtil
{
    public static Vector3Int AsXZ(this Vector2Int v, int y = 0) {
        return new Vector3Int(v.x, y, v.y);
    }
}
