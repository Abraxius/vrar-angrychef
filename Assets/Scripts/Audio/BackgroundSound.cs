using System.Collections;
using System.Collections.Generic;
using AngryChief.Manager;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.m_FunLevel)
        {
            AudioManager.Instance.Play("background_mcdonalds");
        }
        else
        {
            AudioManager.Instance.Play("background_jazz");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
