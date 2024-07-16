using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockUpgrade : Upgrade
{

    public StockUpgrade() : base ("1UP", 250, 99999) { }
    public override void Apply()
    {
        GameManager.Instance.m_Life++;
    }
}
