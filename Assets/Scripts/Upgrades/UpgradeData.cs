using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeData
{
    public string Name;
    public int Level;

    public UpgradeData(string name, int level)
    {
        Name = name;
        Level = level;
    }
}
