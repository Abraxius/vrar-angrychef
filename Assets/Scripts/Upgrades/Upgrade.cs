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
    private int costIncrement;


    public Upgrade(string name, int cost, int costIncrement)
    {
        Name = name;
        Cost = cost;
        initialCost = cost;
        this.costIncrement = costIncrement;
    }

    public abstract void Apply();

    public virtual void IncreaseCost()
    {
        Cost += costIncrement;
    }
    
    public void ResetUpgrade()
    {
        Cost = initialCost;
    }
}
