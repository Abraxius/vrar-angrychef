using System;
using UnityEngine;

public class AdjustPanelSize : MonoBehaviour
{
    // Das UI-Panel, das angepasst werden soll
    public RectTransform uiPanel;
    public RectTransform uiPanelWithChilds;

    public float heightForEveryObject;
    
    // Aktualisiert die Größe des Panels basierend auf den GameObjects als Childs
    public void UpdatePanelSize()
    {
        // Hole alle Child-Objekte (nicht-UI-Elemente)
        int childCount = uiPanelWithChilds.childCount;

        float totalHeight = 0f;

        // Gehe durch jedes Child-Objekt
        for (int i = 0; i < childCount; i++)
        {
            totalHeight += heightForEveryObject;
        }
        
        // Sicherheitsabfrage, dass das UI nicht in die Decke geht
        if (totalHeight > 420f)
            totalHeight = 420f;
        
        // Aktualisiere die Höhe des Panels basierend auf der Gesamthöhe der Childs
        uiPanel.sizeDelta = new Vector2(uiPanel.sizeDelta.x, totalHeight);
    }
}