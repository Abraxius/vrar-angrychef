using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

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

public class Recipe
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
    
    public List<Ingredient> Ingredients;

    public Dictionary<Ingredient, int> MaxIngredientCount
    {
        get => maxIngredientCount;
        set => maxIngredientCount = value;
    }

    public int MaxTotalIngredientCount
    {
        get => maxTotalIngredientCount;
        set => maxTotalIngredientCount = value;
    }
    #endregion
    
#region Public Methods

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
