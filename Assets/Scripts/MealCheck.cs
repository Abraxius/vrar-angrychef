using System.Collections.Generic;
using UnityEngine;

public class MealCheck : MonoBehaviour
{
    public static bool MealCorrectlyCooked(Recipe recipe, List<Ingredient> snappedIngredients)
    {
        if (recipe.Ingredients.Count == snappedIngredients.Count)
        {
            for (int i = 0; i < snappedIngredients.Count; i++)
            {
                if (snappedIngredients[i].ingredientType != recipe.Ingredients[i])
                {
                    return false;
                }
            }
            return true;
        }
        
        return false;
    }
}
