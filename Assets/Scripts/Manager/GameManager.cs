using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int m_Life;
    public int m_Ammunition;

    public int m_Money;
    public int m_Diamands;

    public int m_CurrentLevel;

    int m_StartClients;
    int m_CurrentClients;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(this);
        }
    }

    public void StartGame()
    {
        //ToDo: Später lade Perma HP, ...
        ChangeScene("GameScene");
    }

    public void ContinueGame(int level)
    {
        //ToDo: Verwende Upgrades
    }

    void DayEnd()
    {
        m_CurrentLevel += 1;
        m_CurrentClients += m_StartClients; //ToDo: Hier Upgrade integrieren für mehr Kunden
    }

    void ChangeScene(string value)
    {
        SceneManager.LoadScene(value);
    }
}
