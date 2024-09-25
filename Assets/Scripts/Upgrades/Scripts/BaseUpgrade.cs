using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUpgrade : ScriptableObject
{
    public string m_UpgradeName;
    public int m_Cost { get; protected set; }
    public int m_InitialCost;
    public int m_Level { get; private set; }
    public int m_MaxLevel;
    
    // Virtuelle Methode, die von Subklassen überschrieben wird
    public abstract void Apply();
    
    public virtual void IncreaseCost()
    {
        // m_MaxLevel == -1 für unendliche Upgrades
        if (m_MaxLevel != -1)
        {
            if (m_Level < m_MaxLevel)
            {
                m_Level++;
                UpdateCost();
            }
            else
            {
                //What happens if max level is reached.
            }
        }
    }
    private void UpdateCost()
    {
        m_Cost = m_InitialCost * (m_Level + 1);
    }

    public void ResetUpgrade()
    {
        m_Level = 0;
        m_Cost = m_InitialCost;
        UpdateCost();
    }

    public void SetLevel(int level)
    {
        m_Level = level;
        UpdateCost();
    }
}
