using System;
using UnityEngine;

namespace UI
{
    public class HandMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_ConfirmUI;
        [SerializeField] private GameObject m_HandMenu;

        private GameInput _gameInput;

        private bool m_Visible = false;
        private void Awake()
        {
            _gameInput = new GameInput();
            
            CloseConfirm();
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
            if (_gameInput.Player.HandMenuOpen.triggered)
            {
                m_Visible = !m_Visible;
                
                m_HandMenu.SetActive(m_Visible);
                m_ConfirmUI.SetActive(false);     
                
                Debug.Log("Open Menu");
            }
        }

        public void OpenConfirm()
        {
           m_HandMenu.SetActive(false);
           m_ConfirmUI.SetActive(true);
           
           Debug.Log("Pressed");
        }

        public void CloseConfirm()
        {
            m_HandMenu.SetActive(false);
            m_ConfirmUI.SetActive(false);     
        }
        
        public void OpenMainMenu()
        {
            GameManager.Instance.LoadScene("StartScene");
        }
    }
}
