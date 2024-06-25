using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Info_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public GameManager gameManager;
    private int lives;
    private int ammo;
    private int money;
    private int diamands;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameManager = GameManager.Instance;
        lives = gameManager.m_Life;
        ammo = gameManager.m_Ammunition;
        money = gameManager.m_Money;
        diamands = gameManager.m_Diamands;

        text.text =
    "- Lives: " + lives.ToString() + "\n" +
    "- Ammo: " + ammo.ToString() + "\n" +
    "- Money: " + money.ToString() + "\n" +
    "- Diamonds: " + diamands.ToString();
    }

}
