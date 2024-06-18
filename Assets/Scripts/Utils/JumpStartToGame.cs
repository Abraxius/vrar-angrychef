using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStartToGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
