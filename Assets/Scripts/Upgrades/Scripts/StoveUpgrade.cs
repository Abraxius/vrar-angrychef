using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoveUpgrade", menuName = "Upgrades/Stove")]
public class StoveUpgrade : BaseUpgrade
{
    // ‹berschreibe die Methode mit spezifischer Funktionalitšt
    public override void Apply()
    {
        GameManager.Instance.m_StoveLevel++;
    }
}
