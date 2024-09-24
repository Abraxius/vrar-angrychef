using System.Collections.Generic;
using UnityEngine;

public class Meal
{
    public List<Ingredient> Ingredients;
}

public class IngredientSnapping : MonoBehaviour
{
    [HideInInspector]
    public bool snapped = false;
    //public List<Ingredient> snappedIngredients = new List<Ingredient>();
    public Meal snappedIngredients = new Meal { Ingredients = new List<Ingredient>() };
    public bool stackable = false;
    public Transform snapPosition;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Snappable")
        {
            if ((snappedIngredients.Ingredients.Count == 0 && !other.gameObject.GetComponent<Ingredient>().isSnapped) || (snappedIngredients.Ingredients.Count >= 1 && stackable == true && !other.gameObject.GetComponent<Ingredient>().isSnapped))
            {
                SnapObject(other.gameObject);
                AdjustColliderSize();
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
        snappedIngredients.Ingredients.Add(ingredient);

        // Deactivate Rigidbody/physics
        Rigidbody rb = snappableObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        // Set position and rotation of snapped object
        if (stackable)
        {
            float totalHeight = 0f;
            for (int i = 0; i < snappedIngredients.Ingredients.Count - 1; i++)
            {
                totalHeight += snappedIngredients.Ingredients[i].GetHeight();
            }
            float currentHeight = ingredient.GetHeight();
            print("Current Height: " + currentHeight);
            print("Total Height: " + totalHeight);
            Vector3 position = gameObject.transform.position + new Vector3(0, totalHeight + (currentHeight / 2), 0);
            print("Position: " + position);
            snappableObject.transform.position = position;
            snappableObject.transform.rotation = Quaternion.identity;
        }
        else
        {
            snappableObject.transform.position = snapPosition.position;
            snappableObject.transform.rotation = Quaternion.identity;
        }
    }

    private void AdjustColliderSize()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            float totalHeight = 0f;
            foreach (Ingredient ingredient in snappedIngredients.Ingredients)
            {
                totalHeight += ingredient.GetHeight();
            }
            collider.size = new Vector3(collider.size.x, totalHeight + 0.7f, collider.size.z);
            collider.center = new Vector3(collider.center.x, (totalHeight / 2), collider.center.z);

            // Adjust the position to move the collider upwards
            collider.center = new Vector3(collider.center.x, collider.size.y / 2, collider.center.z);
        }
    }
}
