using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Instance;

    //private Dictionary<string, Upgrade> availableUpgrades;
    public List<BaseUpgrade> m_UpgradeList = new List<BaseUpgrade>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Restart();
        }
        else
        {
            Destroy(this);
        }
    }

    public BaseUpgrade FindObjectByName(string objectName)
    {
        // Durchläuft die Liste der ScriptableObjects
        foreach (BaseUpgrade obj in m_UpgradeList)
        {
            // Vergleicht den Namen des ScriptableObjects mit dem gesuchten Namen
            if (obj.m_UpgradeName == objectName)
            {
                return obj; // Gibt das gefundene Objekt zurück
            }
        }

        Debug.LogWarning($"Object with name {objectName} not found.");
        return null; // Gibt null zurück, wenn kein Objekt gefunden wurde
    }

    public void AllUpgrades()
    {
        foreach (BaseUpgrade obj in m_UpgradeList)
            Debug.Log(obj.m_UpgradeName);
    }

    public void PurchaseUpgradeMoney(string upgradeName)
    {
        BaseUpgrade tmp = FindObjectByName(upgradeName);
        if (tmp != null)
        {
            if (GameManager.Instance.m_Money >= tmp.m_Cost) // Agregar lo del máximo nivel
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
        else
        {
            Debug.Log("Upgrade Missing");
        }
    }

    public void PurchaseUpgradeDiamand(string upgradeName)
    {
        BaseUpgrade tmp = FindObjectByName(upgradeName);
        if (tmp != null)
        {
            if (GameManager.Instance.m_Diamonds >= tmp.m_Cost)
            {
                GameManager.Instance.m_Diamonds -= tmp.m_Cost;
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

    }

    public int GetUpgradeCost(string upgradeName)
    {
        BaseUpgrade tmp = FindObjectByName(upgradeName);
        if (tmp != null)
        {
            return tmp.m_Cost;
        }

        return 0;

    }

    public void SaveUpgrades()
    {
        BaseUpgrade ammo = FindObjectByName("AmmoUpgrade");
        BaseUpgrade mxhp = FindObjectByName("MaxHpUpgrade");
        BaseUpgrade fumode = FindObjectByName("FunModeUpgrade");

        PlayerPrefs.SetInt("AmmoLvl", ammo.m_Level);
        PlayerPrefs.SetInt("HpLvl", mxhp.m_Level);
        PlayerPrefs.SetInt("FunLvl", fumode.m_Level);
        PlayerPrefs.Save();
    }

    public void LoadUpgrades()
    {
        int ammolvl;
        int mxhp;
        int funmode;

        ammolvl = PlayerPrefs.GetInt("AmmoUpgrade", 0);
        mxhp = PlayerPrefs.GetInt("MaxHpUpgrade", 0);
        funmode = PlayerPrefs.GetInt("FunModeUpgrade", 0);

        m_UpgradeList[1].SetLevel(ammolvl);
        m_UpgradeList[8].SetLevel(mxhp);
        m_UpgradeList[5].SetLevel(funmode);
    }

    public void Restart()
    {
        foreach (var upgrade in m_UpgradeList)
        {
            upgrade.ResetUpgrade();
        }
    }


    public void NewRun()
    {
        //Reset only temporary upgrades;
        GameManager.Instance.m_Life = GameManager.Instance.m_MaxLives;
        GameManager.Instance.m_Money = GameManager.Instance.m_StartingMoney;
    }
}