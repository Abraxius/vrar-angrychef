using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CuttinSpeedUpgrade", menuName = "Upgrades/CuttingSpeed")]
public class CuttinSpeedUpgrade : BaseUpgrade
{
    // ‹berschreibe die Methode mit spezifischer Funktionalitšt
    public override void Apply()
    {
        GameManager.Instance.m_CuttingLevel++;
    }
}

