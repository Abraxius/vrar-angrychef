using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Instance;

    public int m_LebenCost = 150;
    public int m_AmmoCost = 50;
    public int m_CustomerCost = 150;
    public float m_KnifeSpeed = 3f;
    public int m_AmmountOfUpgrades = 0;


    private void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ExtraLeben()
    {
        GameManager.Instance.m_Life++; 
        GameManager.Instance.m_MaxLives++;
        GameManager.Instance.m_Money -= m_LebenCost;
        m_LebenCost += 100;
        Debug.Log("Button pressed");

    }

    void ExtraAmmo()
    {
        // To Do: Add Max Ammo limit?
        GameManager.Instance.m_Ammunition++;
        m_AmmoCost += 100;
    }

    void ExtraCustomer()
    {
        // TO DO: add max customers 
        GameManager.Instance.m_DailyMaxCustomer++;
        m_CustomerCost += 100;
        // Intanciate or Activate more chairs

    }

    void KnifeUpgrade()
    {
        // TO DO: Make a formula to make the upgrade of the knife feel better. Maybe a log funct up to a MAX point
        m_KnifeSpeed -= 0.15f;

    }

    void SaveUpgrades()
    {
        //TO DO: add all upgrades and make it so they save. Maybe do scriptable objects
        PlayerPrefs.SetInt("Life", GameManager.Instance.m_Life);
        PlayerPrefs.SetInt("Ammo", GameManager.Instance.m_Ammunition);
        PlayerPrefs.SetInt("Money", GameManager.Instance.m_Life);
        PlayerPrefs.SetInt("Diamands", GameManager.Instance.m_Life);
        PlayerPrefs.SetInt("MaxCustomers", GameManager.Instance.m_Life);
        PlayerPrefs.SetFloat("Knife", GameManager.Instance.m_Life);
        PlayerPrefs.Save();
    }

    void Restart()
    {

    }
}
