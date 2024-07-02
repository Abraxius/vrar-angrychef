using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// Class that represents a recipe with a list of ingredients
/// </summary>
public class Recipe : IEquatable<Meal>
{
    #region Private Variables

    private static int maxTotalIngredientCount = 7;
    private static Dictionary<IngredientName, int> maxIngredientCount = new Dictionary<IngredientName, int>
    {
        { IngredientName.BunTop, 1 },
        { IngredientName.Onion, 2 },
        { IngredientName.Carrot, 2 },
        { IngredientName.Tomato, 1 },
        { IngredientName.Lettuce, 2 },
        { IngredientName.Burger, 2 },
        { IngredientName.Cheese, 2 },
        { IngredientName.BunBottom, 1 },
    };

    #endregion

    #region Constructor

    public Recipe(List<IngredientName> ingredients)
    {
        this.Ingredients = ingredients;
    }

    #endregion

    #region Public Members

    /// <summary>
    /// List of ingredients
    /// </summary>
    public List<IngredientName> Ingredients;

    /// <summary>
    /// Maximum amount of each ingredient
    /// </summary>
    public Dictionary<IngredientName, int> MaxIngredientCount
    {
        get => maxIngredientCount;
        set => maxIngredientCount = value;
    }

    /// <summary>
    /// Maximum amount of total ingredients in the recipe
    /// </summary>
    public int MaxTotalIngredientCount
    {
        get => maxTotalIngredientCount;
        set => maxTotalIngredientCount = value;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Generates a recipe with random ingredients with top bun and bottom bun in the right place
    /// </summary>
    /// <returns>Recipe object</returns>
    public static Recipe GenerateRecipe()
    {
        List<IngredientName> ingredients = new List<IngredientName>();
        for (int i = 0; i < Enum.GetNames(typeof(IngredientName)).Length; i++)
        {
            int maxCount = maxIngredientCount[(IngredientName)i];
            Random random = new Random();
            int count = random.Next(0, maxCount);
            if (i == (int)IngredientName.BunTop || i == (int)IngredientName.BunBottom)
            {
                count = 1;
            }
            if ((ingredients.Count + count) < (maxTotalIngredientCount - 1) || (i == (int)IngredientName.BunBottom) || (i == (int)IngredientName.BunTop))
            {
                for (int j = 0; j < count; j++)
                {
                    ingredients.Add((IngredientName)i);
                }
            }

        }
        return new Recipe(ingredients);
    }

    /// <summary>
    /// Compare two recipes by their amount and order of ingredients
    /// </summary>
    /// <param name="other">Other Recipe object</param>
    /// <returns>Boolean value if Recipe objects are the same</returns>
    public bool Equals(Meal meal)
    {
        if (meal == null)
        {
            return false;
        }

        if (this.Ingredients.Count != meal.Ingredients.Count)
        {
            return false;
        }

        for (int i = 0; i < this.Ingredients.Count; i++)
        {
            if (this.Ingredients[i] == meal.Ingredients[i].name && meal.Ingredients[i].currentState.stateType == Ingredient.wantedIngredientStateType[this.Ingredients[i]])
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}

public class Order : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var order = Recipe.GenerateRecipe();
        print(order);
    }

    // Update is called once per frame
    void Update()
    {

    }
}