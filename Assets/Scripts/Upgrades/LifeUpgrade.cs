using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpgrade : Upgrade
{
    public LifeUpgrade(int cost, int costIncrement) : base("Life Upgrade", cost, costIncrement)
    {

    }

    public override void Apply()
    {
        GameManager.Instance.m_Life++;
        GameManager.Instance.m_MaxLives++;
    }

    public override void IncreaseCost()
    {
        base.IncreaseCost();
    }
}
