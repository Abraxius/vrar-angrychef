using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngryChief.Customer
{
    public class SittingPlace : MonoBehaviour
    {
        [SerializeField] private GameObject m_PlaceTransform;
        private CustomerSpawnPoint m_CustomerSpawnManager;
        
        // Start is called before the first frame update
        void Start()
        {
            m_CustomerSpawnManager = FindObjectOfType<CustomerSpawnPoint>();
            
            m_CustomerSpawnManager.m_SeatList.Add(new Seat(m_PlaceTransform.transform, false));
            
            Debug.Log(m_CustomerSpawnManager.m_SeatList[0]);
        }
    }
 
}
