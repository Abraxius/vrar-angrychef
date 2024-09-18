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
        public GameObject beef;
        public GameObject cheese;
        public GameObject bunBottom;

        [SerializeField] GameObject m_Bubble; 
        
        [HideInInspector] public Recipe currentOrder;

        public void GenerateOrder()
        {
            currentOrder = Recipe.GenerateRecipe();
            var row = new Vector3(0, 0, 0);
            
            //So that the order is built up from the bottom up and looks nicer in the bubble
            var reverseOrderList = currentOrder.Ingredients;
            reverseOrderList.Reverse();
            
            foreach (var ingredient in reverseOrderList)
            {
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
                        Instantiate(beef, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case IngredientName.Cheese:
                        Instantiate(cheese, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;

                    case IngredientName.BunBottom:
                        Instantiate(bunBottom, gameObject.transform.position + row, Quaternion.identity, gameObject.transform);
                        break;
                }
                
                row += new Vector3(0, 0.2f, 0);
            }

            m_Bubble.SetActive(true);
            m_Bubble.GetComponent<AdjustPanelSize>().UpdatePanelSize();
        }

        public void ClearOrder()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            m_Bubble.SetActive(false);
        }

        void Start()
        {
            m_Bubble.SetActive(false);
            m_yPos = transform.position.y;
        }
        // Update is called once per frame
        void FixedUpdate()
        {

           // var tempPos = transform.position;
            
            //tempPos.y = m_yPos + Mathf.Sin(Time.time) * 0.05f;
           // transform.position = tempPos;
            transform.Rotate(new Vector3(0, 1f, 0));
        }

        void Update()
        {

        }
    }

}