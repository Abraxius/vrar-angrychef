using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : IngredientSnapping
{
    [SerializeField] private float cookTime = 5f; // Time to change from uncooked to cooked
    [SerializeField] private float burnTime = 10f; // Time to change from cooked to burned
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (snappedIngredients.Ingredients.Count == 1)
        {
            timer += Time.deltaTime;

            if (snappedIngredients.Ingredients[0].GetCurrentStateType() == IngredientStateType.Uncooked && timer >= cookTime)
            {
                snappedIngredients.Ingredients[0].SetState(IngredientStateType.Cooked);
                print(snappedIngredients.Ingredients[0].GetCurrentStateType());
            }
            else if (snappedIngredients.Ingredients[0].GetCurrentStateType() == IngredientStateType.Cooked && timer >= burnTime)
            {
                snappedIngredients.Ingredients[0].SetState(IngredientStateType.Burned);
                print(snappedIngredients.Ingredients[0].GetCurrentStateType());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Snappable")
        {
            if (other.gameObject.GetComponent<Ingredient>().fryable && ((snappedIngredients.Ingredients.Count == 0 && !other.gameObject.GetComponent<Ingredient>().isSnapped) || (snappedIngredients.Ingredients.Count >= 1 && stackable == true && !other.gameObject.GetComponent<Ingredient>().isSnapped)))
            {
                SnapObject(other.gameObject);
            }
        }
    }
}