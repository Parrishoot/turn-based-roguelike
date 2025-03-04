using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private bool resetOnDragEnd = true;

    [SerializeField]
    private Transform draggableTransformOverride;

    public EventProcessor OnDragBegin { get; private set; } = new EventProcessor();

    public EventProcessor OnDragEnd { get; private set; } = new EventProcessor();

    protected bool IsDragging  { get; set; } = false;

    protected bool DragEnabled { get; set; } = true;

    protected virtual void Start() {
        if(draggableTransformOverride == null) {
            draggableTransformOverride = transform;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!DragEnabled) {
            return;
        }

        BeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!DragEnabled) {
            return;
        }

        SetPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!DragEnabled) {
            return;
        }

        EndDrag();
    }

    protected virtual void BeginDrag() {
        IsDragging = true;
    }
    
    protected virtual void EndDrag() {
                
        IsDragging = false;
        
        if(resetOnDragEnd) {
            draggableTransformOverride.localPosition = Vector3.zero;
        }
    }

    protected virtual void SetPosition(PointerEventData eventData) {

        if(!IsDragging) {
            return;
        }

        draggableTransformOverride.position = new Vector3(eventData.position.x, eventData.position.y, 0);
    }
}
