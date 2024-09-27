using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField] private float m_Time;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.m_CurrentLevel == 0)
        {
            Debug.Log("UI Timer gestartet");
            StartCoroutine(TimerToClose());
        }
        else
        {
            Debug.Log("UI geschlossen, weil nicht Level 0");
            gameObject.SetActive(false);
        }
    }

    IEnumerator TimerToClose()
    {
        yield return new WaitForSeconds(m_Time);
        gameObject.SetActive(false);  
    }
}
