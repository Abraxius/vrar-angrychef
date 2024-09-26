using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunSpawner : IngredientSpawner
{
    public GameObject bunBottom;
    public GameObject bunTop;

    public override void SpawnIngredient()
    {
        GameObject spawnedBunBottom = Instantiate(bunBottom);
        spawnedBunBottom.transform.position = new Vector3(0.1f, 2.2f, 6.5f);
        GameObject spawnedBunTop = Instantiate(bunTop);
        spawnedBunTop.transform.position = new Vector3(0.1f, 2.4f, 6.5f);
        Debug.Log("Object spawned");
    }
}
