using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private bool debugFunction;

    [SerializeField] private GameObject m_CheatObject;
    
    private GameInput _gameInput;

    public static DebugManager Instance;
    
    private void Awake()
    {
        m_CheatObject.SetActive(debugFunction);
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(this);
        }
        
        _gameInput = new GameInput();
    }

    private void OnEnable()
    {
        _gameInput.Player.Enable();
    }

    private void OnDisable()
    {
        _gameInput.Player.Disable();
    }
    
    private void Update()
    {
        if (_gameInput.Player.CheatSystem.triggered)
        {
            debugFunction = !debugFunction;
            m_CheatObject.SetActive(debugFunction);
            Debug.Log("CheatSystem: " + debugFunction);  
        }
        
        if (debugFunction)
        {
            if (_gameInput.Player.LoadEndScene.triggered)
            {
                GameManager.Instance.LoadScene("EndScene");
                Debug.Log("P is pressed");
            }
            if (_gameInput.Player.Mama.triggered)
            {
                GameManager.Instance.m_Diamonds += 100;
                Debug.Log("Mama kommt vorbei und bringt die 100 Diamanten!");
            }
            if (_gameInput.Player.Motherlode.triggered)
            {
                GameManager.Instance.m_Money += 100;
                GameManager.Instance.m_Score += 100;
                Debug.Log("Motherlode, du erhälst 100 Dollar, du Rabauke!");
            }
            if (_gameInput.Player.SpawnEat.triggered)
            {
                if (GameManager.Instance.m_CustomersList[0] == null)
                    return;
                
                GameManager.Instance.m_CustomersList[0].FinishOrder();
                
                Debug.Log("Simsalabim, wie von Zauberhand erscheint das richtige Rezept!");
            }
        }
    }
}
