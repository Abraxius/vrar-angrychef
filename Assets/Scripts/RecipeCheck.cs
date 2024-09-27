using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngryChief.Cook;
using AngryChief.Customer;
using AngryChief.Manager;
using Random = System.Random;

public class RecipeCheck : MonoBehaviour
{
    ShowOrder m_ShowOrder;
    Recipe m_Recipe;
    Meal meal;

    private void Awake()
    {
        m_ShowOrder = FindAnyObjectByType<ShowOrder>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Get current Recipe
        m_Recipe = m_ShowOrder.currentOrder;
        Debug.Log(m_Recipe == null);

        // Check if gameobject is a Meal and if there is a current Order
        if (other.gameObject.tag == "Meal" && m_Recipe != null)
        {
            this.meal = other.gameObject.transform.GetChild(0).gameObject.GetComponent<IngredientSnapping>().snappedIngredients;
            
            Debug.Log(m_Recipe.Ingredients);
            if(m_Recipe.Equals(meal))
            {
                Debug.Log("Meal is correct!");
                // Finish Order and Destroy Meal
                Destroy(other.gameObject);

                if (GameManager.Instance.m_FunLevel)
                {
                    AudioManager.Instance.Play("fun_order_finish_good" + new Random().Next(1,2)); // TODO: Update count
                }
                
                GameManager.Instance.m_CustomersList[0].FinishOrder();
            }
            else
            {
                Debug.Log("Meal is wrong!");
                Destroy(other.gameObject);
                
                if (GameManager.Instance.m_FunLevel)
                {
                    AudioManager.Instance.Play("fun_order_finish_bad" + new Random().Next(1,3)); // TODO: Update count
                }
                
                GameManager.Instance.m_CustomersList[0].LoseOrder();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
