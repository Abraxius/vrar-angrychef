using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUpgrade : Upgrade
{
    public AmmoUpgrade() : base("MaxAmmo", 5, 5)
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
