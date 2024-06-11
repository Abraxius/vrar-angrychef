using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngryChief.Cook
{
    public class ShowOrder : MonoBehaviour
    {
        public GameObject bunTop;
        public GameObject onion;
        public GameObject tomato;
        public GameObject carrot;
        public GameObject salad;
        public GameObject steak;
        public GameObject beef;
        public GameObject cheese;
        public GameObject bunBottom;

        public void GenerateOrder()
        {
            var newRecipe = Recipe.GenerateRecipe();
            var row = new Vector3(0, 1.0f, 0);
            foreach (var ingredient in newRecipe.Ingredients)
            {
                row -= new Vector3(0, 0.2f, 0);

                switch (ingredient)
                {
                    case Ingredient.BunTop:
                        Instantiate(bunTop, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case Ingredient.Onion:
                        Instantiate(onion, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case Ingredient.Tomato:
                        Instantiate(tomato, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case Ingredient.Carrot:
                        Instantiate(carrot, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case Ingredient.Salad:
                        Instantiate(salad, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case Ingredient.Steak:
                        Instantiate(steak, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case Ingredient.Beef:
                        Instantiate(beef, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case Ingredient.Cheese:
                        Instantiate(cheese, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case Ingredient.BunBottom:
                        Instantiate(bunBottom, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0, 0.1f, 0));
        }
    }

}
