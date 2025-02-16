using System;
using UnityEngine;

public class MySelectable : MonoBehaviour
{
    public Action OnHoverStart { get; set; }
    
    public Action OnHoverEnd { get; set; }

    public Action OnSelect { get; set; }

    public Action<bool> OnSelectableSet { get; set; }

    public bool IsSelectable { get; private set; } = true;

    protected bool AllowHoverOnUnselectable { get; set; } = false;

    protected virtual void ProcessSelect() {
        OnSelect?.Invoke();
    }

    protected void OnMouseDown()
    {
        if(!IsSelectable) {
            return;
        }

        ProcessSelect();
    }

    protected void OnMouseEnter() {

        if(!IsSelectable && !AllowHoverOnUnselectable) {
            return;
        }

        OnHoverStart?.Invoke();
    }

    protected void OnMouseExit() {

        if(!IsSelectable && !AllowHoverOnUnselectable) {
            return;
        }

        OnHoverEnd?.Invoke();
    }

    public void SetSelectable(bool isSelectable) {
        IsSelectable = isSelectable;
        OnSelectableSet?.Invoke(isSelectable);
    }
}
