using System;
using UnityEngine;

namespace AngryChief.Cook
{
    public class ShowOrder : MonoBehaviour
    {
        private float m_yPos = 0.0f;

        public GameObject bunTop;
        public GameObject onion;
        public GameObject tomato;
        public GameObject carrot;
        public GameObject salad;
        public GameObject steak;
        public GameObject beef;
        public GameObject cheese;
        public GameObject bunBottom;

        [HideInInspector] Recipe currentOrder;

        public void GenerateOrder()
        {
            Debug.Log("saasda");
            currentOrder = Recipe.GenerateRecipe();
            var row = new Vector3(0, 1.0f, 0);
            foreach (var ingredient in currentOrder.Ingredients)
            {
                row -= new Vector3(0, 0.2f, 0);

                switch (ingredient)
                {
                    case IngredientName.BunTop:
                        Instantiate(bunTop, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case IngredientName.Onion:
                        Instantiate(onion, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case IngredientName.Tomato:
                        Instantiate(tomato, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case IngredientName.Carrot:
                        Instantiate(carrot, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case IngredientName.Lettuce:
                        Instantiate(salad, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case IngredientName.Burger:
                        Instantiate(steak, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case IngredientName.Cheese:
                        Instantiate(cheese, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case IngredientName.BunBottom:
                        Instantiate(bunBottom, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                }
            }
        }

        public void ClearOrder()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        void Start()
        {
            m_yPos = transform.position.y;
            GenerateOrder();
        }
        // Update is called once per frame
        void FixedUpdate()
        {

            var tempPos = transform.position;

            tempPos.y = m_yPos + Mathf.Sin(Time.time) * 0.1f;
            transform.position = tempPos;
            transform.Rotate(new Vector3(0, 1f, 0));
        }

        void Update()
        {

        }
    }

}