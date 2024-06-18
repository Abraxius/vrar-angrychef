using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class IngredientState
{
    public string stateName;
    public GameObject stateObject;
}

public class Ingredient : MonoBehaviour
{
    // Set states and possibilities for further processing
    public bool fryable = false;
    public bool cuttable = false;
    public IngredientType ingredientType;
    public List<IngredientState> ingredientStates = new List<IngredientState>();
    [HideInInspector]
    public IngredientState currentState;

    // For Snapping System
    [HideInInspector]
    public bool isSnapped = false;

    void Start()
    {
        if (ingredientStates.Count > 0)
        {
            SetState(ingredientStates[0].stateName); // Set initial state to the first state in the list
        }
    }

    public void SetState(string stateName)
    {
        foreach (var state in ingredientStates)
        {
            if (state.stateName == stateName)
            {
                currentState = state;
                break;
            }
        }

        // Deactivate all state objects
        foreach (var state in ingredientStates)
        {
            state.stateObject.SetActive(false);
        }

        // Activate the corresponding state object
        if (currentState != null)
        {
            currentState.stateObject.SetActive(true);
        }
    }

    public string GetCurrentState()
    {
        return currentState != null ? currentState.stateName : null;
    }

    // Get height of ingredient
    public float GetHeight()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            return collider.bounds.size.y;
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.size.y;
        }

        // Return default height if no collider or renderer is found
        return 1.0f;
    }
}
