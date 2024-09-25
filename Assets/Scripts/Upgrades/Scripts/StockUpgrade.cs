using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StockUpgrade", menuName = "Upgrades/Stock")]
public class Stock : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_Life++;
    }
}
