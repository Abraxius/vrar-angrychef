using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AngryChief.Customer;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int m_Life;
    public int m_Ammunition;

    public int m_Money;
    public int m_Diamands;

    public int m_CurrentLevel;

    public int m_StartCustomer;
    public int m_CurrentCustomer;
    public int m_DailyMaxCustomer = 3;

    public int m_CurrentWaitingCustomer;

    public List<CustomerController> m_CustomersList = new List<CustomerController>();


    // Dieser bool-Wert gibt an, ob die Szene vollständig geladen ist
    private bool isSceneLoaded = false;

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

    public void LoadScene(string value)
    {
        StartCoroutine(LoadSceneAsync(value));
    }

    public void StartGame()
    {
        //ToDo: Später lade Perma HP, ...
        //ChangeScene("GameScene");

        StartCoroutine(LoadSceneAsync("GameScene"));
        //GameScene
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //yield return new WaitForSeconds(2); //Billig test erstmal

        isSceneLoaded = true;
        OnSceneLoaded();
    }

    void OnSceneLoaded()
    {
        if (isSceneLoaded)
        {
            Debug.Log("Szene vollständig geladen!");

            CustomerSpawnPoint customerSpawn = FindObjectOfType<CustomerSpawnPoint>();
            
            if (customerSpawn != null)
            {
                if (m_CurrentLevel == 0)
                    customerSpawn.StarGame();
                else
                    customerSpawn.ContinueGame(m_CurrentLevel);
            }
            isSceneLoaded = false;
        }
    }

    public void ContinueGame(int level)
    {
        //ToDo: Verwende Upgrades
    }

    void DayEnd()
    {
        m_CurrentLevel += 1;
        m_DailyMaxCustomer += m_StartCustomer; //ToDo: Hier Upgrade integrieren für mehr Kunden
    }



    void ChangeScene(string value)
    {
        SceneManager.LoadScene(value);
    }
}
