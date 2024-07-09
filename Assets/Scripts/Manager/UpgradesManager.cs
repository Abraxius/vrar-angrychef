using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Instance;

    public int m_LebenCost = 150;
    public int m_AmmoCost = 50;
    public int m_CustomerCost = 150;
    public float m_KnifeSpeed = 3f;
    public int m_AmmountOfUpgrades = 0;

    private Dictionary<string, Upgrade> availableUpgrades;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeUpgrades();
        }
        else
        {
            Destroy(this);
        }
    }

    public void InitializeUpgrades()
    {
        availableUpgrades = new Dictionary<string, Upgrade>
        {
            { "MaxLives", new LifeUpgrade(150, 150) },
            { "MaxAmmo", new AmmoUpgrade(250, 250) }
            // TO DO: add all upgrades
        };
    }

    public void PurchaseUpgradeMoney(string upgradeName)
    {
        if (availableUpgrades.ContainsKey(upgradeName))
        {
            Upgrade upgrade = availableUpgrades[upgradeName];
            if (GameManager.Instance.m_Money >= upgrade.Cost)
            {
                GameManager.Instance.m_Money -= upgrade.Cost;
                upgrade.Apply();
                upgrade.IncreaseCost();
            }
            else
            {
                //No Money Logic
                Debug.Log("No Money");
            }
        }
        else
        {
            Debug.Log("Upgrade Missing");
        }
    }

    public void PurchaseUpgradeDiamand(string upgradeName)
    {
        if (availableUpgrades.ContainsKey(upgradeName))
        {
            Upgrade upgrade = availableUpgrades[upgradeName];
            if (GameManager.Instance.m_Diamands >= upgrade.Cost)
            {
                GameManager.Instance.m_Diamands -= upgrade.Cost;
                upgrade.Apply();
                upgrade.IncreaseCost();
            }
            else
            {
                //No Money Logic
                Debug.Log("No Diamands");
            }
        }
        else
        {
            Debug.Log("Upgrade Missing");
        }
    }

    public int GetUpgradeCost(string upgradeName)
    {
        if (availableUpgrades.ContainsKey(upgradeName))
        {
            return availableUpgrades[upgradeName].Cost;
        }
        return 0;

    }

    public void SaveUpgrades()
    {
        #region PERMAUPGRADES

        PlayerPrefs.SetInt("MaxLife", GameManager.Instance.m_MaxLives);
        PlayerPrefs.SetInt("MaxAmmo", GameManager.Instance.m_MaxAmmo);
        PlayerPrefs.SetInt("Diamands", GameManager.Instance.m_Diamands);
        PlayerPrefs.Save();
            
        #endregion


        //PlayerPrefs.SetInt("Money", GameManager.Instance.m_Money);
        //PlayerPrefs.SetInt("MaxCustomers", GameManager.Instance.m_DailyMaxCustomer);

    }

    public void Restart()
    {
        //Reset upgrade Cost
        //foreach .... upgrade.ResetUpgrade();
    }

    public void LoadUpgrades()
    {
        GameManager.Instance.m_MaxLives =  PlayerPrefs.GetInt("MaxLife", GameManager.Instance.m_MaxLives);
        GameManager.Instance.m_MaxAmmo = PlayerPrefs.GetInt("MaxAmmo", GameManager.Instance.m_MaxAmmo);
        GameManager.Instance.m_Diamands = PlayerPrefs.GetInt("Diamands", GameManager.Instance.m_Diamands);
        //...?
    }

    public void NewRun()
    {
        //Reset only temporary upgrades;
        GameManager.Instance.m_Life = GameManager.Instance.m_MaxLives;
        GameManager.Instance.m_Money = GameManager.Instance.m_StartingMoney;
        //Messer
        //Herd
        //Mehr Zutaten
        //...?
    }

    
}
