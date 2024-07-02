using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    public TextMeshProUGUI costText;
    public string upgradeName;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCostText(upgradeName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Upgrade(int upgradenr)
    {
        int money = GameManager.Instance.m_Money;
        int diamands = GameManager.Instance.m_Diamands;
    }

    public void UpdateCostText(string upgradeName)
    {
        int cost = UpgradesManager.Instance.GetUpgradeCost(upgradeName);
        costText.text = $"Cost: {cost}";
    }

    public void BuyUpgradeMoney(string upgradeName)
    {
            UpgradesManager.Instance.PurchaseUpgradeMoney(upgradeName);   
    }

    public void BuyUpgradeDiamands(string upgradeName)
    {
        UpgradesManager.Instance.PurchaseUpgradeDiamand(upgradeName);
    }
}


