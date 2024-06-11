using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowOrder : MonoBehaviour
{

    public GameObject canvas;

    public GameObject bunTop;
    public GameObject onion;
    public GameObject tomato;
    public GameObject carrot;
    public GameObject salad;
    public GameObject steak;
    public GameObject beef;
    public GameObject cheese;
    public GameObject bunBottom;


    // Start is called before the first frame update
    void Start()
    {
        var newRecipe = Recipe.GenerateRecipe();
        var row = new Vector3(0, 1.0f, 0);
        foreach (var ingredient in newRecipe.Ingredients)
        {
            row -= new Vector3(0, 0.2f, 0);

            switch (ingredient)
            {
                case Ingredient.BunTop:
                    Instantiate(bunTop, canvas.transform.position + row, Quaternion.identity, canvas.transform);
                    break;

                case Ingredient.Onion:
                    Instantiate(onion, canvas.transform.position + row, Quaternion.identity, canvas.transform);
                    break;

                case Ingredient.Tomato:
                    Instantiate(tomato, canvas.transform.position + row, Quaternion.identity, canvas.transform);
                    break;

                case Ingredient.Carrot:
                    Instantiate(carrot, canvas.transform.position + row, Quaternion.identity, canvas.transform);
                    break;

                case Ingredient.Salad:
                    Instantiate(salad, canvas.transform.position + row, Quaternion.identity, canvas.transform);
                    break;

                case Ingredient.Steak:
                    Instantiate(steak, canvas.transform.position + row, Quaternion.identity, canvas.transform);
                    break;

                case Ingredient.Beef:
                    Instantiate(beef, canvas.transform.position + row, Quaternion.identity, canvas.transform);
                    break;

                case Ingredient.Cheese:
                    Instantiate(cheese, canvas.transform.position + row, Quaternion.identity, canvas.transform);
                    break;

                case Ingredient.BunBottom:
                    Instantiate(bunBottom, canvas.transform.position + row, Quaternion.identity, canvas.transform);
                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
