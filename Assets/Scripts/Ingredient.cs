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
    public List<IngredientState> ingredientStates = new List<IngredientState>();
    private IngredientState currentState;

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
}
