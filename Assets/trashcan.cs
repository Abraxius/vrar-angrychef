using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashcan : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Snappable" || other.gameObject.tag == "Meal")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.transform.parent.gameObject.tag == "Snappable")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
