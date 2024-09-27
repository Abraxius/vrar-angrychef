using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameScene : MonoBehaviour
{
    public void LoadGame()
    {
        GameManager.Instance.StartGame();
    }
}
