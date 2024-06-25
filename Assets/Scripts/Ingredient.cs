using UnityEngine;
using System.Collections.Generic;

public enum IngredientName
{
    Lettuce,
    Tomato,
    Onion,
    Cheese,
    Carrot,
    Burger,
    Bun_Top,
    Bun_Bottom,
}

public enum IngredientStateType
{
    Uncooked,
    Cooked,
    Burned,
    Whole,
    Slice,
    Chopped
}

[System.Serializable]
public class IngredientState
{
    public IngredientStateType stateType;
    public GameObject stateObject;
}

public class Ingredient : MonoBehaviour
{
    // Set states and possibilities for further processing
    public bool fryable = false;
    public bool cuttable = false;
    public IngredientName name;
    public List<IngredientState> ingredientStates = new List<IngredientState>();
    private IngredientState currentState;

    // For Snapping System
    [HideInInspector]
    public bool isSnapped = false;

    void Start()
    {
        if (ingredientStates.Count > 0)
        {
            SetState(ingredientStates[0].stateType); // Set initial state to the first state in the list
        }
    }

    public void SetState(IngredientStateType stateType)
    {
        foreach (var state in ingredientStates)
        {
            if (state.stateType == stateType)
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

    public IngredientStateType GetCurrentStateType()
    {
        //return currentState != null ? currentState.stateType : null;
        return currentState.stateType;
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
