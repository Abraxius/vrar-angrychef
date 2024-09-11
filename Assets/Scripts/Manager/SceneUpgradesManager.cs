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

    public NavMeshSurface m_NavMesh;
    
    private int m_HouseLevel = 0;
    private int m_TableLevel = 0;
    
    private void Awake()
    {
        m_HouseLevel = GameManager.Instance.m_HouseLevel;
        m_TableLevel = GameManager.Instance.m_TableLevel;
        
        if (m_HouseLevel != 0)
        {
            m_HouseGameObjects[0].SetActive(false);     
            m_HouseGameObjects[m_HouseLevel].SetActive(true);   
        }

        if (m_TableLevel > 0)
        {
            for (int i = 3; i < m_TableLevel + 3; i++)
            {   
                m_TableObjects[m_TableLevel].SetActive(true);       
            }
        }
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
