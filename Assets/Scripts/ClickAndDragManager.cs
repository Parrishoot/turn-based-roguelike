using UnityEditor;
using UnityEngine;

public class ClickAndDragManager : MonoBehaviour
{
    private Vector2Int closestCell = Vector2Int.zero;

    [Header("Debug")]

    [SerializeField]
    private bool showClosestCellOnGizmos = true;

    [SerializeField]
    private bool highlightSpaceGizmo = true;

    // Update is called once per frame
    void Update()
    {
        closestCell = BoardManager
            .Instance
            .Grid
            .GetClosestCellViaScreen(Input.mousePosition);
    }

    private void OnDrawGizmos() {
        
        if(showClosestCellOnGizmos) {
            Handles.Label(transform.position, closestCell.ToString(), GuiStyleUtil.GetDefaultHandleStyle());
        }

        if(highlightSpaceGizmo && BoardManager.Instance != null) {

            Color color = Color.yellow;
            color.a = .5f;
            Gizmos.color = color;

            Gizmos.DrawCube(BoardManager.Instance.Grid.GetCellCenterWorld(closestCell.x, closestCell.y), Vector3.one * .8f);
        }
    }
}
