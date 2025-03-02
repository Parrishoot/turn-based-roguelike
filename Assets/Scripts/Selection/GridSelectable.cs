using System;
using UnityEngine;

public class GridSelectable : MySelectable
{
    public BoardSpace Space { get; set; }

    public Action OnDeselect;

    public bool Selected { get; private set; } = false;

    protected override bool AllowHoverOnUnselectable => true;

    protected void Start() {
        SetSelectable(false);
    }

    protected override void ProcessSelect()
    {
        SelectionManager.Instance.CheckSelection(this);
    }

    public void Select() {
        Selected = true;
        OnSelect?.Invoke();
    }


    public void Deselect() {
        Selected = false;
        OnDeselect?.Invoke();
    }
}
