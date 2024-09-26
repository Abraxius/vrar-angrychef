using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ingredient;

    public virtual void SpawnIngredient()
    {
        GameObject spawnedIngredient = Instantiate(ingredient);
        spawnedIngredient.transform.position = new Vector3(0.1f, 2.2f, 6.5f);
        Debug.Log("Object spawned");
    }
}
