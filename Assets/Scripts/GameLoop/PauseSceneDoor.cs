using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Is there so that the next level can be started via the trigger in the pause scene
/// </summary>
public class PauseSceneDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            GameManager.Instance.ContinueGame(0);   
        }
    }
}
