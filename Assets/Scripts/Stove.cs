using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : IngredientsSnapping
{
    [SerializeField] private float cookTime = 5f; // Time to change from uncooked to cooked
    [SerializeField] private float burnTime = 10f; // Time to change from cooked to burned
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (snappedIngredients.Count == 1)
        {
            timer += Time.deltaTime;
            print(snappedIngredients[0].GetCurrentState());

            if (snappedIngredients[0].GetCurrentState() == "uncooked" && timer >= cookTime)
            {
                snappedIngredients[0].SetState("cooked");
            }
            else if (snappedIngredients[0].GetCurrentState() == "cooked" && timer >= burnTime)
            {
                snappedIngredients[0].SetState("burned");
            }
        }
    }
}
