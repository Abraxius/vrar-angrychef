using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoUpgrade", menuName = "Upgrades/Ammo")]
public class Ammo : BaseUpgrade
{
    // ‹berschreibe die Methode mit spezifischer Funktionalitšt
    public override void Apply()
    {
        GameManager.Instance.m_MaxAmmo += 2;
    }
}
