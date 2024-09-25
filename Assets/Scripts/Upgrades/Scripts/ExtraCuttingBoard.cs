using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExtraCuttingBoard", menuName = "Upgrades/ExtraBoard")]
public class ExtraCuttingBoard : BaseUpgrade
{
    // �berschreibe die Methode mit spezifischer Funktionalit�t
    public override void Apply()
    {
        GameManager.Instance.m_CuttingTable = true;
    }
}
