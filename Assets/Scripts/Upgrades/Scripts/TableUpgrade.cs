using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TableUpgrade", menuName = "Upgrades/Tables")]
public class TableUpgrade : BaseUpgrade
{
    // ‹berschreibe die Methode mit spezifischer Funktionalitšt
    public override void Apply()
    {
        GameManager.Instance.m_TableLevel++;
    }
}
