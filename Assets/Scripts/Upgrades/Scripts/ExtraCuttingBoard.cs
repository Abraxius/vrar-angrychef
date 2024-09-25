using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExtraCuttingBoard", menuName = "Upgrades/ExtraBoard")]
public class ExtraCuttingBoard : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_CuttingTable = true;
    }
}
