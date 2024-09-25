using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdsUpgrade", menuName = "Upgrades/AdUpgrade")]
public class AdsUpgrade : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_AdvertismentLevel++;
    }
}
