using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngryChief.Customer
{
    public class Seat
    {
        public Transform m_Transform;
        public bool m_Busy;
        
        public Seat(Transform transform, bool busy)
        {
            m_Transform = transform;
            m_Busy = busy;
        }
    }
    
    public class CustomerSpawnPoint : MonoBehaviour
    {
        public List<Seat> m_SeatList = new List<Seat>();
        [SerializeField] GameObject m_CashDeskPosition;
        [SerializeField] GameObject m_Customer;
        
        int m_CurrentCustomer = 0;
        int m_DailyMaxCustomer;

        public void StarGame()
        {
            m_DailyMaxCustomer = GameManager.Instance.m_DailyMaxCustomer;

            StartCoroutine(SpawnCustomer());
        }

        public void ContinueGame(int level)
        {
            //m_Daily = last Stand
        }

        IEnumerator SpawnCustomer()
        {
            float waitTime = Random.Range(2f, 5f); //ToDo: muss mit dem level mitskalieren

            // Ausgabe der Wartezeit in der Konsole (optional)
            Debug.Log("Wartezeit: " + waitTime + " Sekunden");

            yield return new WaitForSeconds(waitTime);

            GameObject tmp = GameObject.Instantiate(m_Customer, transform.position, transform.rotation);

            CustomerController customer = tmp.GetComponent<CustomerController>();
            customer.m_Target = m_CashDeskPosition.transform;
            
            if (GameManager.Instance.m_CurrentWaitingCustomer == 0)
            {
                customer.m_Position = m_CashDeskPosition.transform.position;
            }
            else
            {
                customer.m_Position = customer.m_Target.position + new Vector3(0, 0, -GameManager.Instance.m_CurrentWaitingCustomer * 1.5f);
            }

            customer.m_CustomerSpawnManager = this;
            customer.WalkToCashDesk();
            customer.m_WaitingPosition = m_CurrentCustomer;
            customer.m_OldWaitingPosition = m_CurrentCustomer;

            GameManager.Instance.m_CustomersList.Add(customer);

            m_CurrentCustomer += 1;

            GameManager.Instance.m_CurrentWaitingCustomer += 1;

            if (m_CurrentCustomer < m_DailyMaxCustomer)
                StartCoroutine(SpawnCustomer());
        }
    }

}