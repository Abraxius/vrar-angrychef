using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUpgrade : Upgrade
{
    public AmmoUpgrade(int cost, int costIncrement) : base("Ammo Upgrade", cost, costIncrement)
    {

    }

    public override void Apply()
    {
        GameManager.Instance.m_Ammunition += 2;
        GameManager.Instance.m_MaxAmmo += 2;
    }

    public override void IncreaseCost()
    {
        base.IncreaseCost();
    }
}
