using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script vor a visuel time
/// </summary>
public class TimeClock : MonoBehaviour
{
    private bool busy;
    private float finishTime;
    private float elapsedTime;

    public bool onlyEatTimer;
    /// <summary>
    /// If time = 0 it is only decoration
    /// </summary>
    /// <param name="time"></param>
    public void SetTime(float time)
    {
        var totalRotation = (time / time) * 360f; // Berechnung der Gesamtrotation (30 Sekunden = 180 Grad)

        transform.rotation = Quaternion.Euler(0, 0, totalRotation);
        elapsedTime = 0;
        
        finishTime = time;
        busy = true;
    }
    
    void FixedUpdate()
    {
        if (!busy)
            return;
        
        elapsedTime += Time.fixedDeltaTime;
        
        float rotationAmount = 360f / finishTime * Time.fixedDeltaTime;
        transform.Rotate(new Vector3(0,0, -rotationAmount));

        if (onlyEatTimer)
            return;
        
        if (elapsedTime >= finishTime)
        {
            GameManager.Instance.m_CustomersList[0].LoseOrder();
        }
    }
}
