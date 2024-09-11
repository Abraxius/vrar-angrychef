using System;
using System.Collections;
using System.Collections.Generic;
using AngryChief.Manager;
using UnityEngine;
using UnityEngine.UI;

public class ControlUi : MonoBehaviour
{
    [SerializeField] private GameObject[] m_UiElements;

    private int m_OldChildValue = 0;
    private int m_CurrentChildValue = 0;

    [SerializeField] private GameObject m_Music;
    [SerializeField] private GameObject m_Sound;
    public void Awake()
    {
        for (int i = 1; i < m_UiElements.Length; i++)
        {
          m_UiElements[i].SetActive(false);  
        }
    }

    /// <summary>
    /// Wird im Inspector aufgerufen
    /// </summary>
    public void ChangeOpenUIElement(int childValue)
    {
        m_UiElements[m_CurrentChildValue].SetActive(false);
        m_OldChildValue = m_CurrentChildValue;
        m_UiElements[childValue].SetActive(true);
        m_CurrentChildValue = childValue;
    }

    /// <summary>
    /// Wird im Inspector aufgerufen
    /// </summary>
    public void GoBack()
    {
        m_UiElements[m_CurrentChildValue].SetActive(false);
        m_UiElements[m_OldChildValue].SetActive(true);
        int tmp = m_CurrentChildValue;
        m_CurrentChildValue = m_OldChildValue;
        m_OldChildValue = tmp;
    }

    /// <summary>
    /// Wird im Inspector aufgerufen
    /// </summary>
    public void ChangeMusicVolume(float value)
    {   
        AudioManager.Instance.ChangeMusicVolume(value);   
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    /// <summary>
    /// Wird im Inspector aufgerufen
    /// </summary>
    public void ChangeSoundVolume(float value)
    {
        AudioManager.Instance.ChangeSoundVolume(value);          
        PlayerPrefs.SetFloat("soundVolume", value);
    }

    /// <summary>
    /// Wird im Inspector aufgerufen
    /// </summary>
    public void ChangeFunMode(bool value)
    {
        GameManager.Instance.m_FunLevel = value;
    }

    /// <summary>
    /// Wird im Inspector aufgerufen
    /// </summary>
    public void SetSliders()
    {
        if (AudioManager.Instance == null)
            return;
        
        if (m_Music != null)
            m_Music.GetComponent<Slider>().value = PlayerPrefs.GetFloat("musicVolume");
        
        if (m_Sound != null)
            m_Sound.GetComponent<Slider>().value = PlayerPrefs.GetFloat("soundVolume");
    }
}
