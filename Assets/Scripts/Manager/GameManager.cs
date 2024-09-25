using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AngryChief.Customer;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int m_Life;
    public int m_MaxLives;
    public int m_MaxAmmo = 1;
    public int m_Ammunition;

    [HideInInspector] public int m_Score;
    [HideInInspector] public int m_ScoreDiamonds;
    public int m_Money;
    [FormerlySerializedAs("m_Diamands")] public int m_Diamonds;
    public int m_StartingMoney = 500;

    public int m_CurrentLevel;

    private int m_StandardDailyMaxCustomer;
    private int m_StandardLengthCustomerQueue;
    
    public int m_DailyMaxCustomer = 4;
    public int m_LengthCustomerQueue = 3;

    public int m_CurrentWaitingCustomer;
    public int m_AllGuestsVisitedToday;
    
    public List<CustomerController> m_CustomersList = new List<CustomerController>();

    public bool m_FunLevelBuyed; //Ist Fun Level verfügbar? Also gekauft?
    public bool m_FunLevel = false;
    
    public int m_CurrentPrices = 10;

    //GameObject Upgrades
    public int m_HouseLevel = 0;

    public int m_StoveLevel = 0;
    public int m_TableLevel;

    public bool m_IngredientCarrot;
    public bool m_IngredientTomato;
    public bool m_IngredientLettuce;
    public bool m_IngredientOnion;

    public bool m_CuttingTable;
    public int m_CuttingLevel;
    
    public int m_CookingLevel;
    public int m_WaitingTimeLevel; //Kunden warten länger
    public int m_AdvertismentLevel; //More Customers
    public int m_QualityLevel; //More moneyprices for eat
    
    public float m_TimeForOrder = 60f;
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
        //UpgradesManager.Instance.LoadUpgrades();
    }

    private void OnApplicationQuit()
    {
        //UpgradesManager.Instance.SaveUpgrades();
    }

    public void LoadScene(string value)
    {
        StartCoroutine(LoadSceneAsync(value));
    }

    public void StartGame()
    {
        //ToDo: Später lade Perma HP, ...

        m_StandardDailyMaxCustomer = m_DailyMaxCustomer;
        m_StandardLengthCustomerQueue = m_LengthCustomerQueue;

        m_Score = 0;
        
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
        m_DailyMaxCustomer += 3; 
        m_CustomersList.Clear();
        
        StartCoroutine(LoadSceneAsync("HassGameScene"));
    }

    /// <summary>
    /// ToDo: Muss getriggert werden, wenn alle Leben verbraucht sind also x falsche Rezepte gemacht wurden
    /// </summary>
    public void LoseGame()
    {
        m_AllGuestsVisitedToday = 0;
        m_CurrentLevel = 0;
        m_DailyMaxCustomer = m_StandardDailyMaxCustomer;
        m_CustomersList.Clear();
        
        m_HouseLevel = 0;
        m_StoveLevel = 0;
        m_IngredientCarrot = false;
        m_IngredientTomato = false;
        m_IngredientLettuce = false;
        m_IngredientOnion = false;
        m_TableLevel = 0;
        m_CuttingTable = false;
        m_CuttingLevel = 0;
        
        m_LengthCustomerQueue = m_StandardLengthCustomerQueue;
        
        m_Money = m_StartingMoney;
        
        m_ScoreDiamonds = m_Score / 100;
        m_Diamonds += m_ScoreDiamonds;

        ChangeScene("EndScene");
    }

    [ContextMenu("Reset Upgrades to Level 0")]
    public void ResetUpgrades()
    {
        UpgradesManager.Instance.Restart();
        Debug.Log("Upgrades have been reset to level 0.");
    }

    void ChangeScene(string value)
    {
        SceneManager.LoadScene(value);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
