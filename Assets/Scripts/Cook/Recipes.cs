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
        //seed random
        Random random = new Random();

        //Get random number for max total ingredient count of recipe
        var recipeTotalIngredientCount = random.Next(3, maxTotalIngredientCount);

        //Create a list of ingredients for recipe
        List<IngredientName> ingredients = new List<IngredientName>();

        // Add bun top to the list
        ingredients.Add(IngredientName.BunTop);

        // Iterate through each ingredient name except bun top and bun bottom and fill the sandwich
        for (int i = 0; i < Enum.GetNames(typeof(IngredientName)).Length; i++)
        {
            // Skip bun top and bun bottom
            if ((i != (int)IngredientName.BunTop && i != (int)IngredientName.BunBottom))
            {
                // Get max count of ingredient
                int maxCount = maxIngredientCount[(IngredientName)i];

                // Get random count of ingredient
                int count = random.Next(0, maxCount);

                // if total ingredient count is less than max total ingredient count, add ingredient to list
                if ((ingredients.Count + count) < (recipeTotalIngredientCount - 1))
                {
                    for (int j = 0; j < count; j++)
                    {
                        ingredients.Add((IngredientName)i);
                    }
                }
            }
        }

        // Add bun bottom to list
        ingredients.Add(IngredientName.BunBottom);

        ingredients.Reverse();

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
            Debug.Log("Meal is null");
        }

        if (this.Ingredients.Count != meal.Ingredients.Count)
        {
            Debug.Log("Ingredient Count is wrong.");
            return false;
        }

        for (int i = 0; i < this.Ingredients.Count; i++)
        {
            if (Enum.GetName(typeof(IngredientName), this.Ingredients[i]) == Enum.GetName(typeof(IngredientName), meal.Ingredients[i].name))// && meal.Ingredients[i].currentState.stateType == Ingredient.wantedIngredientStateType[this.Ingredients[i]])
            {
                return true;
            }
            Debug.Log("Failed on:" + Enum.GetName(typeof(IngredientName), this.Ingredients[i])+ "  " + Enum.GetName(typeof(IngredientName), meal.Ingredients[i].name));
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
