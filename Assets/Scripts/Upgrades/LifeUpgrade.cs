using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpgrade : Upgrade
{
    public LifeUpgrade() : base("MaxLives", 10, 10)
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
