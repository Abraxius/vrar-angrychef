using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatienceUpgrade", menuName = "Upgrades/Patience")]
public class PatienceUpgrade : BaseUpgrade
{
    // ‹berschreibe die Methode mit spezifischer Funktionalitšt
    public override void Apply()
    {
        GameManager.Instance.m_WaitingTimeLevel++;
    }
}
