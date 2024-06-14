using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSnapping : MonoBehaviour
{
    [HideInInspector]
    public bool snapped = false;
    public List<Ingredient> snappedIngredients = new List<Ingredient>();
    public bool stackable = false;
    [SerializeField] private Transform snapPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Snappable")
        {
            if ((snappedIngredients.Count == 0 && !other.gameObject.GetComponent<Ingredient>().isSnapped) || (snappedIngredients.Count >= 1 && stackable == true && !other.gameObject.GetComponent<Ingredient>().isSnapped))
            {
                SnapObject(other.gameObject);
            }
        }
    }

    protected void SnapObject(GameObject SnappableObject)
    {
        print("Snapped");
        snapped = true;
        SnappableObject.GetComponent<Ingredient>().isSnapped = true;
        SnappableObject.transform.SetParent(gameObject.transform);
        snappedIngredients.Add(SnappableObject.GetComponent<Ingredient>());

        // deactivate rigidbody/physics
        Rigidbody rb = SnappableObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        SnappableObject.transform.position = snapPosition.position + new Vector3(0, (0.1f + ((snappedIngredients.Count - 1) * 0.25f)), 0);
        SnappableObject.transform.rotation = Quaternion.identity;
    }
}
