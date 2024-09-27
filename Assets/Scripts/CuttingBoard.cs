using System.Collections;
using System.Collections.Generic;
using AngryChief.Manager;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit;

public class CuttingBoard : MonoBehaviour
{
    [HideInInspector]
    public bool snapped = false;
    public Meal snappedIngredients = new Meal { Ingredients = new List<Ingredient>() };
    public bool stackable = false;
    public Transform snapPosition;
    [SerializeField] private float choppTime = 2f; // Time to change from whole to chopped
    [SerializeField] private float sliceTime = 4f; // Time to change from chopped to sliced
    private float timer = 0f;
    private GameObject snappedIngredient;
    private bool canSnapAgain = true;   // Flag, um eine kurze Verz�gerung nach dem Herausnehmen zu erm�glichen
    private bool canTake = false;

    // Update is called once per frame
    void Update()
    {
        if (snappedIngredients.Ingredients.Count == 1)
        {
            timer += Time.deltaTime;

            if (snappedIngredients.Ingredients[0].GetCurrentStateType() == IngredientStateType.Whole && timer >= choppTime)
            {
                snappedIngredients.Ingredients[0].SetState(IngredientStateType.Chopped);
                // print(snappedIngredients.Ingredients[0].GetCurrentStateType());
                AudioManager.Instance.Stop("knife");
                AudioManager.Instance.Play("success");
            }
            else if (snappedIngredients.Ingredients[0].GetCurrentStateType() == IngredientStateType.Chopped && timer >= sliceTime)
            {
                snappedIngredients.Ingredients[0].SetState(IngredientStateType.Slice);
                // print(snappedIngredients.Ingredients[0].GetCurrentStateType());
                AudioManager.Instance.Stop("chop");
                AudioManager.Instance.Stop("knife");
                AudioManager.Instance.Play("success");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.Play("knife");
        if (canSnapAgain)
        {
            if (other.gameObject.transform.parent != null)
            {
                if (other.gameObject.transform.parent.gameObject.tag == "Snappable" && !snapped)
                {
                    if (other.gameObject.transform.parent.gameObject.GetComponent<Ingredient>().cuttable && ((snappedIngredients.Ingredients.Count == 0 && !other.gameObject.transform.parent.gameObject.GetComponent<Ingredient>().isSnapped) || (snappedIngredients.Ingredients.Count >= 1 && stackable == true && !other.gameObject.transform.parent.gameObject.GetComponent<Ingredient>().isSnapped)))
                    {
                        StartCoroutine(TakeCooldown());

                        Debug.Log("Zutat aufs Schneidebrett gelegt");
                        AudioManager.Instance.Play("chop");
                        SnapObject(other.gameObject.transform.parent.gameObject);
                        snappedIngredient = other.gameObject.transform.parent.gameObject;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canTake)
        {
            if (other.gameObject.transform.parent.gameObject.Equals(snappedIngredient) && snapped)
            {
                // Starte Coroutine, um zu verhindern, dass der Burger sofort wieder gesnapped wird
                StartCoroutine(SnapCooldown());

                TakeObject(other.gameObject.transform.parent.gameObject);
                Debug.Log("Zutat vom Schneidebrett genommen");
                AudioManager.Instance.Stop("chop");
            }
            else
            {
                Debug.Log("Nicht die aktuelle Zutat");
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
        snappedIngredient = null;
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
