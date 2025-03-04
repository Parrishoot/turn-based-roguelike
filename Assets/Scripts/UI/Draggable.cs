using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private bool resetOnDragEnd = true;

    public EventProcessor OnDragBegin { get; private set; } = new EventProcessor();

    public EventProcessor OnDragEnd { get; private set; } = new EventProcessor();

    protected bool IsDragging  { get; set; } = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }

    protected virtual void BeginDrag() {
        IsDragging = true;
    }
    
    protected virtual void EndDrag() {
                
        IsDragging = false;
        
        if(resetOnDragEnd) {
            transform.localPosition = Vector3.zero;
        }
    }

    protected virtual void SetPosition(PointerEventData eventData) {

        if(!IsDragging) {
            return;
        }

        transform.position = new Vector3(eventData.position.x, eventData.position.y, 0);
    }
}
