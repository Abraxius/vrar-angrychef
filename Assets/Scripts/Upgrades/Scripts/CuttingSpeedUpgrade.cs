using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CuttinSpeedUpgrade", menuName = "Upgrades/CuttingSpeed")]
public class CuttinSpeedUpgrade : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_CuttingLevel++;
    }
}

