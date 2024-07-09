using System.Collections;
using System.Collections.Generic;
using AngryChief.Manager;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Play("mcdonalds");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
