using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QualityUpgrade", menuName = "Upgrades/Quality")]
public class QualityUpgrade : BaseUpgrade
{
    // ‹berschreibe die Methode mit spezifischer Funktionalitšt
    public override void Apply()
    {
        GameManager.Instance.m_QualityLevel++;
    }
}
