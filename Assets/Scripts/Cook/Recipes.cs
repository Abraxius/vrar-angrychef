using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// Enum that represents ingredients to choose
/// </summary>
public enum Ingredient
{
    BunTop,
    Onion,
    Tomato,
    Carrot,
    Salad,
    Steak,
    Beef,
    Cheese,
    BunBottom,
}

/// <summary>
/// Class that represents a recipe with a list of ingredients
/// </summary>
public class Recipe : IEquatable<Recipe>
{
    #region Private Variables

    private static int maxTotalIngredientCount = 7;
    private static Dictionary<Ingredient, int> maxIngredientCount = new Dictionary<Ingredient, int>
    {
        { Ingredient.BunTop, 1 },
        { Ingredient.Onion, 2 },
        { Ingredient.Carrot, 2 },
        { Ingredient.Tomato, 1 },
        { Ingredient.Salad, 2 },
        { Ingredient.Steak, 2 },
        { Ingredient.Cheese, 2 },
        { Ingredient.Beef, 2 },
        { Ingredient.BunBottom, 1 },
    };

    #endregion

    #region Constructor

    public Recipe(List<Ingredient> ingredients)
    {
        this.Ingredients = ingredients;
    }

    #endregion

    #region Public Members

    /// <summary>
    /// List of ingredients
    /// </summary>
    public List<Ingredient> Ingredients;

    /// <summary>
    /// Maximum amount of each ingredient
    /// </summary>
    public Dictionary<Ingredient, int> MaxIngredientCount
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
        List<Ingredient> ingredients = new List<Ingredient>();
        for (int i = 0; i < Enum.GetNames(typeof(Ingredient)).Length; i++)
        {
            int maxCount = maxIngredientCount[(Ingredient)i];
            Random random = new Random();
            int count = random.Next(0, maxCount);
            if (i == (int)Ingredient.BunTop || i == (int)Ingredient.BunBottom)
            {
                count = 1;
            }
            if ((ingredients.Count + count) < (maxTotalIngredientCount - 1) || (i == (int)Ingredient.BunBottom) || (i == (int)Ingredient.BunTop))
            {
                for (int j = 0; j < count; j++)
                {
                    ingredients.Add((Ingredient)i);
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
    public bool Equals(Recipe other)
    {
        if (other == null)
        {
            return false;
        }

        if (this.Ingredients.Count != other.Ingredients.Count)
        {
            return false;
        }

        for (int i = 0; i < this.Ingredients.Count; i++)
        {
            if (this.Ingredients[i] != other.Ingredients[i])
            {
                return false;
            }
        }

        return true;
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
