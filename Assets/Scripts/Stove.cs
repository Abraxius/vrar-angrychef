using System.Collections;
using System.Collections.Generic;
using AngryChief.Manager;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit;

public class Stove : MonoBehaviour
{
    [HideInInspector]
    public bool snapped = false;
    public Meal snappedIngredients = new Meal { Ingredients = new List<Ingredient>() };
    public bool stackable = false;
    public Transform snapPosition;
    [SerializeField] private float cookTime = 5f; // Time to change from uncooked to cooked
    [SerializeField] private float burnTime = 10f; // Time to change from cooked to burned
    private float timer = 0f;
    private GameObject snappedBurger;
    private bool canSnapAgain = true;   // Flag, um eine kurze Verz�gerung nach dem Herausnehmen zu erm�glichen
    private bool canTake = false;

    // Update is called once per frame
    void Update()
    {
        if (snappedIngredients.Ingredients.Count == 1)
        {
            timer += Time.deltaTime;

            if (snappedIngredients.Ingredients[0].GetCurrentStateType() == IngredientStateType.Uncooked && timer >= cookTime)
            {
                snappedIngredients.Ingredients[0].SetState(IngredientStateType.Cooked);
                AudioManager.Instance.Play("success");
                // print(snappedIngredients.Ingredients[0].GetCurrentStateType());
            }
            else if (snappedIngredients.Ingredients[0].GetCurrentStateType() == IngredientStateType.Cooked && timer >= burnTime)
            {
                snappedIngredients.Ingredients[0].SetState(IngredientStateType.Burned);
                AudioManager.Instance.Stop("fry");
                AudioManager.Instance.Play("alarm");
                // print(snappedIngredients.Ingredients[0].GetCurrentStateType());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canSnapAgain)
        {
            if (other.gameObject.tag == "Snappable" && !snapped)
            {
                if (other.gameObject.GetComponent<Ingredient>().fryable && ((snappedIngredients.Ingredients.Count == 0 && !other.gameObject.GetComponent<Ingredient>().isSnapped) || (snappedIngredients.Ingredients.Count >= 1 && stackable == true && !other.gameObject.GetComponent<Ingredient>().isSnapped)))
                {
                    StartCoroutine(TakeCooldown());

                    Debug.Log("Burger in die Pfanne gelegt");
                    AudioManager.Instance.Play("fry");
                    SnapObject(other.gameObject);
                    snappedBurger = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canTake)
        {
            if (other.gameObject.Equals(snappedBurger) && snapped)
            {
                // Starte Coroutine, um zu verhindern, dass der Burger sofort wieder gesnapped wird
                StartCoroutine(SnapCooldown());

                TakeObject(other.gameObject);
                Debug.Log("Burger aus der Pfanne genommen");
                AudioManager.Instance.Stop("fry");
                AudioManager.Instance.Stop("alarm");
            }
            else
            {
                Debug.Log("Nicht der aktuelle Burger");
            }
        }
    }

    protected void SnapObject(GameObject snappableObject)
    {
        print("Snapped");
        snapped = true;
        Ingredient ingredient = snappableObject.GetComponent<Ingredient>();
        ingredient.isSnapped = true;
        snappableObject.transform.SetParent(transform);

        // Deactivate Rigidbody/physics
        Rigidbody rb = snappableObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        snappedIngredients.Ingredients.Add(ingredient);

        snappableObject.transform.SetParent(gameObject.transform);
        Debug.Log(snappableObject);

        snappableObject.transform.position = snapPosition.position;
        snappableObject.transform.rotation = Quaternion.identity;
    }

    private void TakeObject(GameObject snappedObject)
    {
        Debug.Log(snappedObject.gameObject.name);

        Ingredient ingredient = snappedObject.GetComponent<Ingredient>();
        snappedIngredients.Ingredients.Remove(ingredient);


        // Deactivate Rigidbody/physics
        Rigidbody rb = snappedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("Rigidbody!");
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        else
        {
            Debug.Log("Rigidbody is null!");
        }

        ingredient.isSnapped = false;
        snapped = false;
        snappedBurger = null;
    }

    // Coroutine f�r eine kurze Verz�gerung nach dem Herausnehmen
    IEnumerator SnapCooldown()
    {
        canSnapAgain = false;  // Verhindere, dass sofort wieder gesnapped wird
        Debug.Log("Snapping Cooldown started");
        yield return new WaitForSeconds(2f);
        canSnapAgain = true;  // Erlaube wieder das Snapping
        Debug.Log("Snapping Cooldown finished");
    }

    IEnumerator TakeCooldown()
    {
        canTake = false;  // Verhindere, dass sofort wieder gesnapped wird
        Debug.Log("Take Cooldown started");
        yield return new WaitForSeconds(1f);
        canTake = true;  // Erlaube wieder das Snapping
        Debug.Log("Take Cooldown finished");
    }
}