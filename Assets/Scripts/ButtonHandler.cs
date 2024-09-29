using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Text costText;
    public string upgradeName;

    // Start is called before the first frame update
    void Update()
    {
        UpdateCostText();
    }

    public void Upgrade(int upgradenr)
    {
        int money = (int)GameManager.Instance.m_Money;
        int diamands = GameManager.Instance.m_Diamonds;
    }

    public void UpdateCostText()
    {
        BaseUpgrade tmp = UpgradesManager.Instance.FindObjectByName(upgradeName);

        if (upgradeName == "TableUpgrade")
        {
            if (GameManager.Instance.m_HouseLevel == 0 )
            {
                costText.text = "Max Tischanzahl für die Restaurantgröße erreicht";
                return;
            }
            if (GameManager.Instance.m_HouseLevel == 1 && GameManager.Instance.m_TableLevel >= 3)
            {
                costText.text = "Max Tischanzahl für die Restaurantgröße erreicht";
                return;
            }
        }

        if (upgradeName == "StockUpgrade" && GameManager.Instance.m_Life >= GameManager.Instance.m_MaxLives)
        {
            costText.text = "Leben sind voll";
            return;
        }


        if (tmp.m_MaxLevel > tmp.m_Level || tmp.m_MaxLevel < 0)
        {
            int cost = UpgradesManager.Instance.GetUpgradeCost(upgradeName);
            costText.text = $"Cost: {cost}";
        }
        else
        {
            costText.text = "Max Level erreicht";
        }
        
    }

    public void BuyUpgradeMoney()
    {
        BaseUpgrade tmp = UpgradesManager.Instance.FindObjectByName(upgradeName);

        if (upgradeName == "TableUpgrade")
        {
            if (GameManager.Instance.m_HouseLevel == 0)
            {
                Debug.Log("Max Tischanzahl für die Restaurantgröße erreicht");
                return;
            }
            if (GameManager.Instance.m_HouseLevel == 1 && GameManager.Instance.m_TableLevel >= 3)
            {
                Debug.Log("Max Tables for restaurant size reached");
                costText.text = "Max Tischanzahl für die Restaurantgröße erreicht";
                return;
            }
        }

        if (upgradeName == "StockUpgrade" && GameManager.Instance.m_Life >= GameManager.Instance.m_MaxLives)
        {
            Debug.Log("At max HP");
            return;
        }

        if (tmp.m_MaxLevel > tmp.m_Level || tmp.m_MaxLevel < 0)
        {
             UpgradesManager.Instance.PurchaseUpgradeMoney(upgradeName);
        }
        else
        {
            //Sound to make the person know its blocked
        }
    }

    public void BuyUpgradeDiamands()
    {
        BaseUpgrade tmp = UpgradesManager.Instance.FindObjectByName(upgradeName);
        if (tmp.m_MaxLevel > tmp.m_Level)
        {
            UpgradesManager.Instance.PurchaseUpgradeDiamand(upgradeName);
        }
        else
        {
            //Sound to make the person know its blocked
        }
    }

    public void DebugUpgrades()
    {
        UpgradesManager.Instance.AllUpgrades();
    }
}


