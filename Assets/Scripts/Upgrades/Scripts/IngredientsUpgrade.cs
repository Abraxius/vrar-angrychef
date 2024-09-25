using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientsUpgrade", menuName = "Upgrades/Ingredients")]
public class IngredientsUpgrade : BaseUpgrade
{
    public override void Apply()
    {
        GameManager.Instance.m_IngredientsLevel++;
        if (m_Level == 0)
        {
            GameManager.Instance.m_IngredientCarrot = true;
        }
        if (m_Level == 1)
        {
            GameManager.Instance.m_IngredientTomato = true;
        }
        if (m_Level == 2)
        {
            GameManager.Instance.m_IngredientLettuce = true;
        }
        if (m_Level == 3)
        {
            GameManager.Instance.m_IngredientOnion = true;
        }
    }
}
