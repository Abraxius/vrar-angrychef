using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : IngredientSnapping
{
    [SerializeField] private float cookTime = 5f; // Time to change from uncooked to cooked
    [SerializeField] private float burnTime = 10f; // Time to change from cooked to burned
    private float timer = 0f;
    private GameObject snappedBurger;
    private bool canSnapAgain = true;   // Flag, um eine kurze Verzögerung nach dem Herausnehmen zu ermöglichen

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
        if (other.gameObject.tag == "Snappable" && !snapped && canSnapAgain)
        {
            if (other.gameObject.GetComponent<Ingredient>().fryable && ((snappedIngredients.Ingredients.Count == 0 && !other.gameObject.GetComponent<Ingredient>().isSnapped) || (snappedIngredients.Ingredients.Count >= 1 && stackable == true && !other.gameObject.GetComponent<Ingredient>().isSnapped)))
            {
                SnapObject(other.gameObject);
                snappedBurger = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        if (other.gameObject.Equals(snappedBurger) && snapped)
        {
            TakeObject(other.gameObject);
            Debug.Log("Burger aus der Pfanne genommen");

            // Starte Coroutine, um zu verhindern, dass der Burger sofort wieder gesnapped wird
            StartCoroutine(SnapCooldown());
        }
        else
        {
            Debug.Log("Nicht der aktuelle Burger");
        }
    }

    private void TakeObject(GameObject snappedObject)
    {
        // Prüfe, ob das Objekt überhaupt einen Parent hat
        if (snappedObject.transform.parent != null)
        {
            Debug.Log("Parent before: " + snappedObject.transform.parent.name);
        }
        else
        {
            Debug.Log("Das Objekt hat keinen Parent.");
        }

        snapped = false;
        snappedBurger = null;
        Ingredient ingredient = snappedObject.GetComponent<Ingredient>();
        ingredient.isSnapped = false;
        snappedObject.transform.parent = null;
        snappedIngredients.Ingredients.Remove(ingredient);


        // Deactivate Rigidbody/physics
        Rigidbody rb = snappedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }

    // Coroutine für eine kurze Verzögerung nach dem Herausnehmen
    IEnumerator SnapCooldown()
    {
        canSnapAgain = false;  // Verhindere, dass sofort wieder gesnapped wird
        yield return new WaitForSeconds(0.5f);  // Warte 0,5 Sekunden
        canSnapAgain = true;  // Erlaube wieder das Snapping
    }
}