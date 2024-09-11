using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AngryChief.Customer;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int m_Life;
    public int m_MaxLives;
    public int m_MaxAmmo = 1;
    public int m_Ammunition;


    public int m_Money;
    public int m_Diamands;
    public int m_StartingMoney = 500;

    public int m_CurrentLevel;
    
    public int m_DailyMaxCustomer = 4;
    public int m_LengthCustomerQueue = 3;

    public int m_CurrentWaitingCustomer;
    public int m_AllGuestsVisitedToday;
    
    public List<CustomerController> m_CustomersList = new List<CustomerController>();

    public bool m_FunLevel = false;
    
    public int m_CurrentPrices = 10;

    //GameObject Upgrades
    public int m_HouseLevel = 0;

    public int m_StoveLevel = 0;
    public int m_TableLevel;

    public int m_IngredientsLevel;
    
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

    private void Start()
    {
        UpgradesManager.Instance.LoadUpgrades();
    }

    private void OnApplicationQuit()
    {
        UpgradesManager.Instance.SaveUpgrades();
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
                customerSpawn.StarGame();
            }
            isSceneLoaded = false;
        }
    }

    public void ContinueGame(int level)
    {
        //ToDo: Verwende Upgrades
        
        StartCoroutine(LoadSceneAsync("GameScene"));    
    }

    public void DayEnd()
    {
        Debug.Log("Day successed!");
        m_AllGuestsVisitedToday = 0;
        m_CurrentLevel += 1;
        m_DailyMaxCustomer += 3; //ToDo: Hier Upgrade integrieren für mehr Kunden
        m_CustomersList.Clear();
        
        StartCoroutine(LoadSceneAsync("HassGameScene"));
    }

    void ChangeScene(string value)
    {
        SceneManager.LoadScene(value);
    }
}
