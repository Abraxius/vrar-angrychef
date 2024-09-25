using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QualityUpgrade", menuName = "Upgrades/Quality")]
public class QualityUpgrade : BaseUpgrade
{
    // �berschreibe die Methode mit spezifischer Funktionalit�t
    public override void Apply()
    {
        GameManager.Instance.m_QualityLevel++;
    }
}
