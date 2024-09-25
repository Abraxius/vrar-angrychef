using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FunModeUpgrade", menuName = "Upgrades/FunMode")]
public class FunModeUpgrade : BaseUpgrade
{
    // �berschreibe die Methode mit spezifischer Funktionalit�t
    public override void Apply()
    {
        GameManager.Instance.m_FunLevelBuyed = true;
    }
}



    
    
    
    
    