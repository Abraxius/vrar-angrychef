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
            this.meal = other.gameObject.transform.GetChild(0).gameObject.GetComponent<IngredientSnapping>().snappedIngredients;
            if(m_ShowOrder.Equals(meal))
            {
                Debug.Log("Meal is correct!");
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("Meal is wrong!");
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
