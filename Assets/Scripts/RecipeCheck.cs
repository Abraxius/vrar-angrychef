using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngryChief.Cook;
using AngryChief.Customer;

public class RecipeCheck : MonoBehaviour
{
    ShowOrder m_ShowOrder;
    Meal meal;
    CustomerController _customerController;
    

    private void Awake()
    {
        
        //m_ShowOrder = FindAnyObjectByType<ShowOrder>();
    }

    private void OnTriggerEnter(Collider other)
    {
 
        
        if (other.gameObject.tag == "Meal")
        {
            _customerController = GameManager.Instance.m_CustomersList[0];
            
            this.meal = other.gameObject.transform.GetChild(0).gameObject.GetComponent<IngredientSnapping>().snappedIngredients;
            
            if(m_ShowOrder.Equals(meal))
            {
                Debug.Log("Meal is correct!");
                _customerController.FinishOrder();

                //Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("Meal is wrong!");
                _customerController.FailOrder();

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
