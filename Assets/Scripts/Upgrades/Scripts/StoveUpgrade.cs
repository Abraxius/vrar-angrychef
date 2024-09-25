using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoveUpgrade", menuName = "Upgrades/Stove")]
public class StoveUpgrade : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_StoveLevel++;
    }
}
