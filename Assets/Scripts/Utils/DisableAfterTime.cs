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
            StartCoroutine(TimerToClose());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator TimerToClose()
    {
        yield return new WaitForSeconds(m_Time);
        gameObject.SetActive(false);  
    }
}
