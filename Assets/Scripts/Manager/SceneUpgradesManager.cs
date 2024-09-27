using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SceneUpgradesManager : MonoBehaviour
{
    public GameObject[] m_TableObjects;
    public GameObject[] m_HouseGameObjects;
    public GameObject[] m_StoveGameObjects;
    public GameObject m_CuttingGameObject;
    public GameObject m_OnionGameObject;
    public GameObject m_TomatoGameObjects;
    public GameObject m_CarrotGameObjects;
    public GameObject m_LettuceGameObjects;

    public NavMeshSurface m_NavMesh;
    
    private int m_HouseLevel = 0;
    private int m_TableLevel = 0;
    private int m_StoveLevel = 0;
    
    private void Awake()
    {
        m_HouseLevel = GameManager.Instance.m_HouseLevel;
        m_TableLevel = GameManager.Instance.m_TableLevel;
        m_StoveLevel = GameManager.Instance.m_StoveLevel;
        
        if (m_HouseLevel != 0)
        {
            m_HouseGameObjects[0].SetActive(false);     
            m_HouseGameObjects[m_HouseLevel].SetActive(true);   
        }

        if (m_TableLevel > 0)
        {
            if (m_HouseLevel == 1)
            {
                if (m_TableLevel > 3)
                    m_TableLevel = 3;
            }
            else if (m_HouseLevel == 2)
            {
                if (m_TableLevel > 6)
                    m_TableLevel = 6;          
            }
            for (int i = 4; i < m_TableLevel + 4; i++)
            {   
                m_TableObjects[i].SetActive(true);       
            }
        }
        
        if (m_StoveLevel != 0)
        {
            m_StoveGameObjects[0].SetActive(false);     
            m_StoveGameObjects[m_StoveLevel].SetActive(true);   
        }
        else
        {
            m_StoveGameObjects[0].SetActive(true);     
            m_StoveGameObjects[m_StoveLevel].SetActive(false);        
        }
        
        if (GameManager.Instance.m_IngredientLettuce)
            m_LettuceGameObjects.SetActive(true);
        
        if (GameManager.Instance.m_IngredientOnion)
            m_OnionGameObject.SetActive(true);
        
        if (GameManager.Instance.m_IngredientCarrot)
            m_CarrotGameObjects.SetActive(true);
        
        if (GameManager.Instance.m_IngredientTomato)
            m_TomatoGameObjects.SetActive(true);
        
        if (GameManager.Instance.m_CuttingTable)
            m_CuttingGameObject.SetActive(true);
    }

    private void Start()
    {
        if (m_HouseLevel > 0)
            RebakeNavMesh();
    }

    // Methode zum Neu-Backen des NavMeshes
    private void RebakeNavMesh()
    {
        if (m_NavMesh != null)
        {
            // NavMesh zur Laufzeit neu backen
            m_NavMesh.BuildNavMesh();
            Debug.Log("NavMesh wurde neu gebacken.");
        }
        else
        {
            Debug.LogError("NavMeshSurface ist nicht zugewiesen!");
        }
    }
}
