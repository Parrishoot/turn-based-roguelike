using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDraggable : Draggable
{
    [field:SerializeReference]
    public CardUIController Card { get; private set; }

    private CardDraggableSocket currentSocket = null;

    void Update()
    {
        CheckForSocket();
    }

    private void CheckForSocket()
    {
        if(!IsDragging) {
            return;
        }

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach(RaycastResult raycastResult in raycastResults) {
            CardDraggableSocket socket = raycastResult.gameObject.GetComponent<CardDraggableSocket>();
            if(socket != null) {
                ProcessSocketEntered(socket, raycastResult.screenPosition);
                return;
            }
        }

        if(currentSocket != null) {
            ProcessSocketExited();
        }
    }

    protected override void BeginDrag()
    {
        EnableSockets();
        base.BeginDrag();
    }

    protected override void EndDrag()
    {
        DisableSockets();

        if(currentSocket != null) {
            currentSocket.ProcessCardInserted(Card);
            return;
        }
    
        base.EndDrag();
    }

    private void ProcessSocketExited() {
        currentSocket = null;
        Card.Grow();
    }

    private void ProcessSocketEntered(CardDraggableSocket socket, Vector2 screenPosition) {

        if(socket == currentSocket) {
            return;
        }

        currentSocket = socket;
        transform.position = screenPosition;
        Card.Shrink();
    }

    private void OnDisable()
    {   
        DisableSockets();
        base.EndDrag();
    }

    private void EnableSockets() {
        foreach(CardDraggableSocket socket in FindObjectsByType<CardDraggableSocket>(FindObjectsSortMode.None)) {
            socket.Show();
        }
    }

    private void DisableSockets() {
        foreach(CardDraggableSocket socket in FindObjectsByType<CardDraggableSocket>(FindObjectsSortMode.None)) {
            socket.Hide();
        }
    }
}
