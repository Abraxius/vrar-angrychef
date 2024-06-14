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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Snappable")
        {
            if (other.gameObject.GetComponent<Ingredient>().fryable && ((snappedIngredients.Count == 0 && !other.gameObject.GetComponent<Ingredient>().isSnapped) || (snappedIngredients.Count >= 1 && stackable == true && !other.gameObject.GetComponent<Ingredient>().isSnapped)))
            {
                SnapObject(other.gameObject);
            }
        }
    }
}
