using System;
using UnityEngine;

public class GridHighlightController : MonoBehaviour
{
    [SerializeField]
    private Color hoveredColor;

    [SerializeField]
    private Color selectedColor;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Color startingColor;

    private GridSelectable selectable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingColor = spriteRenderer.color;

        selectable = GetComponent<GridSelectable>();
        
        if(selectable != null) {
            selectable.OnHoverStart += StartHighlight;
            selectable.OnHoverEnd += EndHighlight;
            selectable.OnSelectableSet += HighlightSelectable;
            selectable.OnDeselect += EndHighlight;
            selectable.OnSelect += HighlightSelected;

            HighlightSelectable(selectable.IsSelectable);
        }       
    }

    private void HighlightSelected()
    {
        spriteRenderer.color = selectedColor;
    }

    private void HighlightSelectable(bool selectable)
    {
        Color color = spriteRenderer.color;
        color.a = selectable ? 1 : 0;

        spriteRenderer.color = color;
    }

    private void EndHighlight()
    {
        if(selectable.Selected) {
            return;
        }

        Color color = startingColor;
        color.a = selectable.IsSelectable ? 1 : 0;

        spriteRenderer.color = color;
    }

    private void StartHighlight()
    {
        if(selectable.Selected) {
            return;
        }

        Color color = hoveredColor;
        color.a = selectable.IsSelectable ? 1 : 0;

        spriteRenderer.color = color;
        
    }
}
