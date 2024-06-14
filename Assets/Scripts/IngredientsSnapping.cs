using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsSnapping : MonoBehaviour
{
    [HideInInspector]
    public bool snapped = false;
    public List<Ingredient> snappedIngredients = new List<Ingredient>();
    [SerializeField] private bool stackable = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Snappable")
        {
            if ((snappedIngredients.Count == 0) || (snappedIngredients.Count >= 1 && stackable == true))
            {
                SnapObject(other.gameObject);
            }
        }
    }

    private void SnapObject(GameObject SnappableObject)
    {
        print("Snapped");
        snapped = true;
        SnappableObject.transform.SetParent(gameObject.transform);
        snappedIngredients.Add(SnappableObject.GetComponent<Ingredient>());

        // deactivate rigidbody/physics
        Rigidbody rb = SnappableObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }
}
