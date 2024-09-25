using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaxHpUpgrade", menuName = "Upgrades/HP")]
public class MaxHpUpgrade : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_Life++;
        GameManager.Instance.m_MaxLives++;
    }
}