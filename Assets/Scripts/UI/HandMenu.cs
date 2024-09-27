using System;
using UnityEngine;

namespace UI
{
    public class HandMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_ConfirmUI;
        [SerializeField] private GameObject m_HandMenu;
        [SerializeField] private GameObject m_OtherUI; //Dieses Element ist in manchen Scenen null, nur in der GameScene ist es belegt
        
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
                OpenMenu();
            }
        }

        public void OpenMenu()
        {
            m_Visible = !m_Visible;
                
            m_HandMenu.SetActive(m_Visible);
            m_ConfirmUI.SetActive(false);     
                
            if (m_OtherUI != null)
                m_OtherUI.SetActive(m_Visible);
            
            Debug.Log("Open Menu");        
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
            
            if (m_OtherUI != null)
                m_OtherUI.SetActive(false);
        }
        
        public void OpenMainMenu()
        {
            GameManager.Instance.LoadScene("StartScene");
        }
    }
}
