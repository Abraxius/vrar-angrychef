using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

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
        //[HideInInspector] public int m_CurrentCustomer = 0;
        int m_DailyMaxCustomer;
        int m_LastSpawnInt = -1;
        
        [SerializeField] GameObject m_CashDeskPosition;
        [SerializeField] GameObject[] m_Customer;
        private bool m_FirstCustomerDone;
        private bool m_IsBusy;
        public void StarGame()
        {
            m_DailyMaxCustomer = GameManager.Instance.m_DailyMaxCustomer + GameManager.Instance.m_AdvertismentLevel; //Upgrade: Max Besucher
            
            StartCoroutine(SpawnCustomer());
        }

        public void ContinueGame(int level)
        {
            //m_Daily = last Stand
        }

        IEnumerator SpawnCustomer()
        {
            if (m_IsBusy)
                yield break;
            
            m_IsBusy = true;
            
            float waitTime = 0f;
            
            if (m_FirstCustomerDone)
                waitTime = Random.Range(10f, 30f);
            else
            {
                waitTime = Random.Range(2f, 5f); 
                m_FirstCustomerDone = true;
            }
            
            // Ausgabe der Wartezeit in der Konsole (optional)
            Debug.Log("Wartezeit: " + waitTime + " Sekunden");

            yield return new WaitForSeconds(waitTime);
            
            //Stellt sicher, dass nicht mehr Kunden als Tische kommen können
            yield return new WaitUntil(() => m_SeatList.Count(seat => !seat.m_Busy) > 0 && GameManager.Instance.m_CustomersList.Count <= m_SeatList.Count);
            
            int randomInt = Random.Range(0, m_Customer.Length);
            
            if (randomInt == m_LastSpawnInt) 
                randomInt = Random.Range(0, m_Customer.Length);
            
            GameObject tmp = GameObject.Instantiate(m_Customer[randomInt], transform.position, transform.rotation);

            m_LastSpawnInt = randomInt;
            
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

            int currentCustomer = GameManager.Instance.m_CurrentWaitingCustomer;
            customer.m_WaitingPosition = currentCustomer;
            customer.m_OldWaitingPosition = currentCustomer;
            
            customer.m_CustomerSpawnManager = this;
            customer.WalkToCashDesk();
            
            GameManager.Instance.m_CustomersList.Add(customer);
            
            GameManager.Instance.m_CurrentWaitingCustomer += 1;
            GameManager.Instance.m_AllGuestsVisitedToday += 1;

            m_IsBusy = false;
            
            CoroutineForSpawn();
        }

        public void CoroutineForSpawn()
        {
            if (GameManager.Instance.m_AllGuestsVisitedToday < m_DailyMaxCustomer && GameManager.Instance.m_CurrentWaitingCustomer < GameManager.Instance.m_LengthCustomerQueue)
                StartCoroutine(SpawnCustomer());
        }
    }

}