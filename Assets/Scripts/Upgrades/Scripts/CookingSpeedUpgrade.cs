using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CookingSpeedUpgrade", menuName = "Upgrades/CookingSpeed")]
public class CookingSpeedUpgrade : BaseUpgrade
{
    // �berschreibe die Methode mit spezifischer Funktionalit�t
    public override void Apply()
    {
        GameManager.Instance.m_CookingLevel++;
    }
}
