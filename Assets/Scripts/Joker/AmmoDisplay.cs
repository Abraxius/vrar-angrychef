using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    public TextMeshProUGUI ammoText;

    private void Start()
    {
        if (ammoText == null)
        {
            ammoText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        UpdateCanvas();
    }
    public void UpdateCanvas()
    {
        if (GameManager.Instance != null)
        {
            ammoText.text = "Ammo: " + GameManager.Instance.m_Ammunition;
        }
    }
}
