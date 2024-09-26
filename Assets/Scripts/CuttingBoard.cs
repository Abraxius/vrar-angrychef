using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : IngredientSnapping
{
    [SerializeField] private float choppTime = 2f; // Time to change from whole to chopped
    [SerializeField] private float sliceTime = 4f; // Time to change from chopped to sliced
    private float timer = 0f;
    private GameObject snappedIngredient;

    // Update is called once per frame
    void Update()
    {
        if (snappedIngredients.Ingredients.Count == 1)
        {
            timer += Time.deltaTime;

            if (snappedIngredients.Ingredients[0].GetCurrentStateType() == IngredientStateType.Whole && timer >= choppTime)
            {
                snappedIngredients.Ingredients[0].SetState(IngredientStateType.Chopped);
                print(snappedIngredients.Ingredients[0].GetCurrentStateType());
            }
            else if (snappedIngredients.Ingredients[0].GetCurrentStateType() == IngredientStateType.Chopped && timer >= sliceTime)
            {
                snappedIngredients.Ingredients[0].SetState(IngredientStateType.Slice);
                print(snappedIngredients.Ingredients[0].GetCurrentStateType());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent != null)
        {
            if (other.gameObject.transform.parent.gameObject.tag == "Snappable" && !snapped)
            {
                if (other.gameObject.transform.parent.gameObject.GetComponent<Ingredient>().cuttable && ((snappedIngredients.Ingredients.Count == 0 && !other.gameObject.transform.parent.gameObject.GetComponent<Ingredient>().isSnapped) || (snappedIngredients.Ingredients.Count >= 1 && stackable == true && !other.gameObject.transform.parent.gameObject.GetComponent<Ingredient>().isSnapped)))
                {
                    SnapObject(other.gameObject.transform.parent.gameObject);
                    snappedIngredient = other.gameObject.transform.parent.gameObject;
                }
            }
        }
    }
}
