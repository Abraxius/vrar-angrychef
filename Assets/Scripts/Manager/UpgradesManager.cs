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

    //private Dictionary<string, Upgrade> availableUpgrades;
    public List<BaseUpgrade> m_UpgradeList = new List<BaseUpgrade>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // InitializeUpgrades();
        }
        else
        {
            Destroy(this);
        }
    }

    /* public void InitializeUpgrades()
     {
         availableUpgrades = new Dictionary<string, Upgrade>
         {
             { "MaxLives", new LifeUpgrade() },
             { "MaxAmmo", new AmmoUpgrade() },
             { "1UP", new StockUpgrade()}
             // TO DO: add all upgrades
         };
     }*/

    public BaseUpgrade FindObjectByName(string objectName)
    {
        // Durchläuft die Liste der ScriptableObjects
        foreach (BaseUpgrade obj in m_UpgradeList)
        {
            // Vergleicht den Namen des ScriptableObjects mit dem gesuchten Namen
            if (obj.name == objectName)
            {
                return obj; // Gibt das gefundene Objekt zurück
            }
        }

        Debug.LogWarning($"Object with name {objectName} not found.");
        return null; // Gibt null zurück, wenn kein Objekt gefunden wurde
    }

    public void PurchaseUpgradeMoney(string upgradeName)
    {
        BaseUpgrade tmp = FindObjectByName(upgradeName);
        if (tmp != null)
        {
            if (GameManager.Instance.m_Money >= tmp.m_Cost)
            {
                GameManager.Instance.m_Money -= tmp.m_Cost;
                tmp.Apply();
                tmp.IncreaseCost();
            }
            else
            {
                //No Money Logic
                Debug.Log("No Money");
            }
        }
        /*if (availableUpgrades.ContainsKey(upgradeName))
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
        }*/
    }

    public void PurchaseUpgradeDiamand(string upgradeName)
    {
        BaseUpgrade tmp = FindObjectByName(upgradeName);
        if (tmp != null)
        {
            if (GameManager.Instance.m_Diamands >= tmp.m_Cost)
            {
                GameManager.Instance.m_Diamands -= tmp.m_Cost;
                tmp.Apply();
                tmp.IncreaseCost();
            }
            else
            {
                //No Diamands Logic
                Debug.Log("No Diamands");
            }
        }
        else
        {
            Debug.Log("Upgrade Missing");
        }

        /*if (availableUpgrades.ContainsKey(upgradeName))
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
                //No Diamands Logic
                Debug.Log("No Diamands");
            }
        }
        else
        {
            Debug.Log("Upgrade Missing");
        }*/
    }

    public int GetUpgradeCost(string upgradeName)
    {
        BaseUpgrade tmp = FindObjectByName(upgradeName);
        if (tmp != null)
        {
            return tmp.m_Cost;
        }

        return 0;
        /*
        if (availableUpgrades.ContainsKey(upgradeName))
        {
            return availableUpgrades[upgradeName].Cost;
        }
        return 0;*/
    }

    public void SaveUpgrades()
    {
        List<UpgradeData> upgradeDataList = new List<UpgradeData>();

        foreach (var upgrade in m_UpgradeList)
        {
            upgradeDataList.Add(new UpgradeData(upgrade.Name, upgrade.m_Level));
        }

        string json = JsonUtility.ToJson(new Serialization<UpgradeData>(upgradeDataList));
        PlayerPrefs.SetString("Upgrades", json);
        PlayerPrefs.Save();
    }

    public void LoadUpgrades()
    {
        if (PlayerPrefs.HasKey("Upgrades"))
        {
            string json = PlayerPrefs.GetString("Upgrades");
            List<UpgradeData> upgradeDataList = JsonUtility.FromJson<Serialization<UpgradeData>>(json).ToList();

            foreach (var data in upgradeDataList)
            {
                BaseUpgrade tmp = FindObjectByName(data.Name);
                if (tmp != null)
                {   
                    tmp.SetLevel(data.Level);
                }
                /*
                if (availableUpgrades.ContainsKey(data.Name))
                {
                    availableUpgrades[data.Name].SetLevel(data.Level);
                }*/
            }
        }
    }

    public void Restart()
    {
        foreach (var upgrade in m_UpgradeList)
        {
            upgrade.ResetUpgrade();
        }
        /*foreach (var upgrade in availableUpgrades.Values)
        {
            upgrade.ResetUpgrade();
        }*/
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