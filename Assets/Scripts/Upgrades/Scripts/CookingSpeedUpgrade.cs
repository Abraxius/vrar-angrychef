using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CookingSpeedUpgrade", menuName = "Upgrades/CookingSpeed")]
public class CookingSpeedUpgrade : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_CookingLevel++;
    }
}
