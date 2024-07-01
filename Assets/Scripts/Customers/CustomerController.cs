using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using AngryChief.Cook;

namespace AngryChief.Customer
{
    public class CustomerController : MonoBehaviour
    {
        public Transform m_Target;
        public Vector3 m_Position;
        public int m_WaitingPosition;
        public int m_OldWaitingPosition;

        [HideInInspector] public CustomerSpawnPoint m_CustomerSpawnManager;
        
        private NavMeshAgent m_Agent;

        Animator m_Animator;
        ShowOrder m_ShowOrder;
        
        public bool m_Destroy = false; //Testobject
        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_ShowOrder = FindAnyObjectByType<ShowOrder>();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            
            StartCoroutine(WalkToCashDesk()); //Nur zum testen
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Destroy)
            {
                //FinishOrder();
                StartCoroutine( Die());
                m_Destroy = false;
            }
        }

        public void StartWalk()
        {
            StartCoroutine(WalkToCashDesk());
        }

        public IEnumerator WalkToCashDesk()
        {
            if (m_Target != null)
            {
                m_Animator.SetBool("Walking", true);
                m_Agent.SetDestination(m_Position);
            }
            else
            {
                yield return null;
            }

            // Warte bis der Agent tats�chlich am Ziel angekommen ist
            while (m_Agent.pathPending || m_Agent.remainingDistance > m_Agent.stoppingDistance || m_Agent.velocity.sqrMagnitude > 0f)
            {
                yield return null;
            }

            m_Animator.SetBool("Walking", false);

            if (m_WaitingPosition == 0)
            {
                transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

                yield return new WaitForSeconds(0.5f);

                m_Animator.SetTrigger("Interact");

                m_ShowOrder.GenerateOrder();
            }
            else
            {
                transform.LookAt(GameManager.Instance.m_CustomersList[0].gameObject.transform);
            }

        }
        
        public IEnumerator WalkToSeat()
        {
            if (m_Target != null)
            {
                m_Animator.SetBool("Walking", true);
                m_Agent.SetDestination(m_Target.position);
            }
            else
            {
                yield return null;
            }

            // Warte bis der Agent tats�chlich am Ziel angekommen ist
            while (m_Agent.pathPending || m_Agent.remainingDistance > m_Agent.stoppingDistance || m_Agent.velocity.sqrMagnitude > 0f)
            {
                yield return null;
            }

            m_Animator.SetBool("Walking", false);

            m_Animator.SetTrigger("SitDown");
            transform.rotation = m_Target.rotation;
        }
        
        public void FinishOrder()
        {
            //Start walking to the seat
            foreach (var seat in m_CustomerSpawnManager.m_SeatList)
            {
                if (!seat.m_Busy)
                {
                    m_Target = seat.m_Transform;
                    seat.m_Busy = true;
                    break;
                }
            }

            GameManager.Instance.m_Money += GameManager.Instance.m_CurrentPrices;
            
            StartCoroutine(WalkToSeat());
            
            if (!CheckWaveIsFinished())
                NextCustomer();
            else
                GameManager.Instance.DayEnd();
        }

        public IEnumerator Die()
        {
            Debug.Log("Gestorben");
            
            m_Animator.SetTrigger("DieTrigger");
            
            //ToDo: Hier Animationsabfrage verhindert, dass es zu Animationsabbrüchen kommt
            yield return new WaitForSeconds(1f);
            
            if (!CheckWaveIsFinished())
                NextCustomer();
            else
                GameManager.Instance.DayEnd();
            
            //ToDo: Neue Leute spawnen
            m_CustomerSpawnManager.CoroutineForSpawn();
            
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Collider>().tag == "Bullet")
            {
                Die();
                Destroy(other.gameObject);
            }
        }
        
        /// <summary>
        /// Start the walk from the queue customer 
        /// </summary>
        void NextCustomer()
        {
            GameManager.Instance.m_CustomersList.Remove(this);
            GameManager.Instance.m_CurrentWaitingCustomer -= 1;
        
            //Start movement from waiting customers
            foreach (var customer in GameManager.Instance.m_CustomersList)
            {
                //customer.m_Target.position = m_Target.position + new Vector3(0, 0, +2f);
                //customer.m_OldWaitingPosition = customer.m_WaitingPosition;
                if (customer.m_WaitingPosition == 0)
                    break;
                
                customer.m_WaitingPosition -= 1;
                
                customer.m_Position = customer.m_Target.position + new Vector3(0, 0, -customer.m_WaitingPosition * 1.5f);
                Debug.Log(" Individuelle Warteposition " + customer.m_WaitingPosition  +" Neue Position: " + customer.m_Position);
                customer.StartWalk();
            }     
        }

        bool CheckWaveIsFinished()
        {
            if (GameManager.Instance.m_CurrentWaitingCustomer == 1 && GameManager.Instance.m_AllGuestsVisitedToday >= GameManager.Instance.m_DailyMaxCustomer)
                return true;
            else
                return false;
        }
    }

}
