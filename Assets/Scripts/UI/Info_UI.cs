using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Info_UI : MonoBehaviour
{
    [SerializeField] Text text;

    public enum Stats { Health, Score, Money, Diamands }
    public Stats statToUpdate;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {

        switch (statToUpdate)
        {
            case Stats.Money:
                UpdateText(GameManager.Instance.m_Money);
                break;
            case Stats.Score:
                UpdateText(GameManager.Instance.m_Score);
                break;
            case Stats.Health:
                UpdateText(GameManager.Instance.m_Life);
                break;
            case Stats.Diamands:
                UpdateText(GameManager.Instance.m_Diamonds);
                break;
            default:
                Debug.Log("Error stat not defined");
                break;
        }
    }

    private void UpdateText(float value)
    {
        text.text = value.ToString();
    }
}

