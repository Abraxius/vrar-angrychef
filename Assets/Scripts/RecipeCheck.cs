using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngryChief.Cook;

public class RecipeCheck : MonoBehaviour
{
    ShowOrder m_ShowOrder;
    Meal meal;

    private void Awake()
    {
        m_ShowOrder = FindAnyObjectByType<ShowOrder>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Meal")
        {
            meal = other.gameObject.transform.GetChild(0).gameObject.GetComponent<IngredientSnapping>().snappedIngredients;
            Debug.Log(meal.Ingredients);
            if(m_ShowOrder.Equals(meal))
            {
                Debug.Log("Meal is correct!");
                //Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("Meal is wrong!");
           
                Destroy(other.gameObject);
            }
            foreach(var ing in meal.Ingredients)
            {
                var ingString = "meal: ";
                ingString += ing.ToString() + " Name: " + ing.name +  " \n";
                Debug.Log(ingString);
            }
            foreach (var ing in m_ShowOrder.currentOrder.Ingredients)
            {
                var ingString = "current: ";
                ingString += ing.ToString() + "Name: "+ ing + " \n";
                Debug.Log(ingString);
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
