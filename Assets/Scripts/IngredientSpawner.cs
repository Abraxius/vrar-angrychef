using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ingredient;
    [SerializeField] private bool isPlate;
    
    public virtual void SpawnIngredient()
    {
        GameObject spawnedIngredient = Instantiate(ingredient);
        if (!isPlate)
            spawnedIngredient.transform.position = new Vector3(0.1f, 2.2f, 6.5f);
        else
            spawnedIngredient.transform.position = new Vector3(-1.4f, 2.2f, 3.5f);
        Debug.Log("Object spawned");
    }
}
