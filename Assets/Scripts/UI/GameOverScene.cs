using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private Text m_Score;

    [SerializeField] private Text m_Diamonds;
    
    // Start is called before the first frame update
    public void SetScoreText()
    {
        var tmp = GameManager.Instance.m_Score;
        Debug.Log(tmp);
        m_Score.text = tmp.ToString();
        m_Diamonds.text = GameManager.Instance.m_ScoreDiamonds.ToString();
    }
}
