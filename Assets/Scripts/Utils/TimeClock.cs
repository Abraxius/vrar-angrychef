using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeClock : MonoBehaviour
{
    // ToDo: Später in diesem Script dafür sorgen, dass auf der Uhr die richtige Zeit angezeigt wird
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,0,Time.deltaTime) * 20f);    
    }
}
