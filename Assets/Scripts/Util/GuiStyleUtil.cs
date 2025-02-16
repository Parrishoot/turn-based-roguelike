using Unity.VisualScripting;
using UnityEngine;

public static class GuiStyleUtil
{
    public static GUIStyle GetDefaultHandleStyle() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
        style.alignment = TextAnchor.LowerCenter;
        style.normal.textColor = Color.white;

        return style;
    }
}
