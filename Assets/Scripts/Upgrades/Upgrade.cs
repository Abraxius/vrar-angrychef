using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade
{
    /* DOCUMENTATION
    
    How to add a new upgrade:
    Create the new upgrade object (c# script)
    There add the values needed "Name, Cost, costIncrement"
    Create the logic in the Apply() method
        This one will be called once the upgrade is bought

    If the upgrade should reset after the player dies, add the name of the upgrade in the UpgradeManager dic in the Restart method.

    When adding the button in the upgrade menu add the function PurchaseUpgradeMoney or PurchaseUpgradeDiamand depending on what currency its used to buy it

    If some info is missing msg Juan.
    */

    public string Name { get; protected set; }
    public int Cost { get; protected set; }
    private int initialCost;
    public int Level { get; private set; }
    private int maxLevel;


    public Upgrade(string name, int initialCost, int maxLevel)
    {
        Name = name;
        this.initialCost = initialCost;
        this.maxLevel = maxLevel;
        Level = 0;
        UpdateCost();
    }

    public abstract void Apply();

    public virtual void IncreaseCost()
    {
        if (Level < maxLevel)
        {
            Level++;
            UpdateCost();
        }
        else
        {
            //What happens if max level is reached.
        }
        
    }
    private void UpdateCost()
    {
        Cost = initialCost * (Level + 1);
    }

    public void ResetUpgrade()
    {
        Level = 0;
        UpdateCost();
    }

    public void SetLevel(int level)
    {
        Level = level;
        UpdateCost();
    }
}
