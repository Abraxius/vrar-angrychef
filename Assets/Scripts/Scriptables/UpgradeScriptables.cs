using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HouseUpgrade", menuName = "Upgrades/House")]
public class House : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_HouseLevel++;    
    }
}

[CreateAssetMenu(fileName = "LifeUpgrade", menuName = "Upgrades/Life")]
public class Life : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_Life++;
        GameManager.Instance.m_MaxLives++;       
    }
}

[CreateAssetMenu(fileName = "AmmoUpgrade", menuName = "Upgrades/Ammo")]
public class Ammo : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_Ammunition += 2;
        GameManager.Instance.m_MaxAmmo += 2; 
    }
}

[CreateAssetMenu(fileName = "StockUpgrade", menuName = "Upgrades/Stock")]
public class Stock : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_Life++;
    }
}

[CreateAssetMenu(fileName = "StoveUpgrade", menuName = "Upgrades/Stove")]
public class StoveUpgrade : BaseUpgrade
{
    // Überschreibe die Methode mit spezifischer Funktionalität
    public override void Apply()
    {
        GameManager.Instance.m_StoveLevel++;    
    }
}