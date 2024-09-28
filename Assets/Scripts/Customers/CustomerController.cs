using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using AngryChief.Cook;
using AngryChief.Manager;
using Random = UnityEngine.Random;

namespace AngryChief.Customer
{
    //ToDo: Diesen Script aufräumen!!!! Alex
    public class CustomerController : MonoBehaviour
    {
        public Transform m_Target;
        public Vector3 m_Position;
        public int m_WaitingPosition;
        public int m_OldWaitingPosition;

        [SerializeField] private GameObject m_WaitingBubblePrefab;
        
        [SerializeField] private GameObject m_BurgerPrefab;
        
        [HideInInspector] public CustomerSpawnPoint m_CustomerSpawnManager;

        [HideInInspector]
        private System.Random m_Random = new System.Random();
        
        private NavMeshAgent m_Agent;

        Animator m_Animator;
        ShowOrder m_ShowOrder;

        private float m_EatingTime = 120f;
        
        private Seat m_CurrentSeat;
        
        private float m_Timer;
        private float m_TargetTime;
        private bool m_WorkOnOrder;
        private GameObject m_Bubble;
        
        public bool m_Destroy = false; //Testobject
        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_ShowOrder = FindAnyObjectByType<ShowOrder>();

            m_TargetTime = GameManager.Instance.m_TimeForOrder;
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
                FinishOrder();
                //StartCoroutine( Die());
                m_Destroy = false;
            }
        }

        public void StartWalk()
        {
            StartCoroutine(WalkToCashDesk());
        }

        public IEnumerator WalkToCashDesk()
        {
            yield return MoveToTarget(m_Position);

            if (m_WaitingPosition == 0)
            {
                transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

                yield return new WaitForSeconds(0.5f);

                m_Animator.SetTrigger("Interact");

                m_ShowOrder.GenerateOrder();

                m_WorkOnOrder = true;
                
                m_Bubble = GameObject.Instantiate(m_WaitingBubblePrefab, transform.position + Vector3.forward * 0.7f + Vector3.up * 1.8f + Vector3.left * 0.5f, transform.rotation);
            
                m_Bubble.GetComponentInChildren<TimeClock>().SetTime(GameManager.Instance.m_TimeForOrder + (GameManager.Instance.m_WaitingTimeLevel * 10) - (GameManager.Instance.m_CurrentLevel * 20)); //Upgrade: Wartezeit 
                
                m_Bubble.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
                
                if (GameManager.Instance.m_FunLevel)
                {
                    AudioManager.Instance.Play("fun_order_" + m_Random.Next(1,3 + 1)); //TODO: Update count of sounds
                }
            }
            else
            {
                transform.LookAt(GameManager.Instance.m_CustomersList[0].gameObject.transform);
            }

        }

        private void FixedUpdate()
        {
            if (m_WorkOnOrder)
            {
                m_Timer += Time.fixedDeltaTime;  // Erhöht den Timer mit der FixedUpdate-Zeit
                if (m_Timer >= m_TargetTime)
                {
                    Destroy(m_Bubble);
                    LoseOrder();
                    m_WorkOnOrder = false;
                }
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

            GameObject tmpBurger = GameObject.Instantiate(m_BurgerPrefab, transform.position + Vector3.forward * 0.7f + Vector3.up, transform.rotation);
            
            GameObject tmpBubble = GameObject.Instantiate(m_WaitingBubblePrefab, transform.position + Vector3.forward * 0.7f + Vector3.up * 1.8f, transform.rotation);
            
            tmpBubble.GetComponentInChildren<TimeClock>().onlyEatTimer = true;
            tmpBubble.GetComponentInChildren<TimeClock>().SetTime(m_EatingTime);
            
            tmpBubble.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            
            yield return new WaitForSeconds(m_EatingTime);
            
            Destroy(tmpBurger);
            Destroy(tmpBubble);

            m_Target = m_CustomerSpawnManager.transform;

            StartCoroutine(LeaveTheKiosk());
        }

        IEnumerator LeaveTheKiosk()
        {
            m_Animator.SetTrigger("StandUp");
            
            yield return new WaitForSeconds(1f);
            
            m_CurrentSeat.m_Busy = false;
            
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
            
            Destroy(gameObject);
        }

        //ToDo: Ersetze die doppelte Codestellen durch das hier
        IEnumerator MoveToTarget(Vector3 targetPosition)
        {
            m_Animator.SetBool("Walking", true);
            m_Agent.SetDestination(targetPosition);

            while (m_Agent.pathPending || m_Agent.remainingDistance > m_Agent.stoppingDistance || m_Agent.velocity.sqrMagnitude > 0f)
            {
                yield return null;
            }

            m_Animator.SetBool("Walking", false);
        }

        IEnumerator LeaveTheKioskWithOutEat()
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
            
            Destroy(gameObject);
        }
        
        //function should be called if the order was true
        public void FinishOrder()
        {
            m_ShowOrder.ClearOrder();
         
            Destroy(m_Bubble);
            m_WorkOnOrder = false;
            
            //Start walking to the seat
            foreach (var seat in m_CustomerSpawnManager.m_SeatList)
            {
                if (!seat.m_Busy)
                {
                    m_Target = seat.m_Transform;
                    seat.m_Busy = true;
                    m_CurrentSeat = seat;
                    break;
                }
            }
            
            float tmpMoney = GameManager.Instance.m_CurrentPrices +
                            (GameManager.Instance.m_CurrentPrices * 
                            (GameManager.Instance.m_IngredientsLevel / 100 * 30) +
                            (GameManager.Instance.m_CurrentPrices * (GameManager.Instance.m_QualityLevel / 100 * 10))); //Upgrade: Mehr Geld
            
            Debug.Log("tmpMoney " + tmpMoney);
            Debug.Log("IngredientLevel " + GameManager.Instance.m_IngredientsLevel);
            Debug.Log("QualityLevel " + GameManager.Instance.m_QualityLevel);
            GameManager.Instance.m_Money += tmpMoney; 
            GameManager.Instance.m_Score += tmpMoney;
            
            StartCoroutine(WalkToSeat());
            
            if (!CheckWaveIsFinished())
                NextCustomer();
            else
            {
                GameManager.Instance.m_CustomersList.Remove(this);
                GameManager.Instance.m_CurrentWaitingCustomer -= 1;
                
                GameManager.Instance.DayEnd();
            }
            
            m_CustomerSpawnManager.CoroutineForSpawn();
        }

        //function should be called if the order was false
        public void LoseOrder()
        { 
            Debug.Log("Bestellung falsch!!!!!");
            
            m_ShowOrder.ClearOrder();
            
            Destroy(m_Bubble);
            m_WorkOnOrder = false;
            
            GameManager.Instance.m_Life -= 1;

            if (GameManager.Instance.m_Life <= 0)
            {
                GameManager.Instance.LoseGame();
            }
            else
            {
                m_Target = m_CustomerSpawnManager.transform;
            
                StartCoroutine(LeaveTheKioskWithOutEat());
            
                if (!CheckWaveIsFinished())
                    NextCustomer();
                else
                {
                    GameManager.Instance.m_CustomersList.Remove(this);
                    GameManager.Instance.m_CurrentWaitingCustomer -= 1;
                
                    GameManager.Instance.DayEnd();
                }
            
                m_CustomerSpawnManager.CoroutineForSpawn();                
            }
        }
        
        public IEnumerator Die()
        {
            Debug.Log("Gestorben");

            Destroy(m_Bubble);
            m_WorkOnOrder = false;
            
            m_ShowOrder.ClearOrder();
            
            m_Animator.SetTrigger("DieTrigger");
            
            //ToDo: Hier Animationsabfrage verhindert, dass es zu Animationsabbrüchen kommt
            yield return new WaitForSeconds(1f);
            
            if (!CheckWaveIsFinished())
                NextCustomer();
            else
            {
                GameManager.Instance.m_CustomersList.Remove(this);
                GameManager.Instance.m_CurrentWaitingCustomer -= 1;
                
                GameManager.Instance.DayEnd();
            }
            
            m_CustomerSpawnManager.CoroutineForSpawn();
            
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Collider>().tag == "Bullet")
            {
                if (m_WaitingPosition == 0)
                    StartCoroutine(Die());
                
                Destroy(other.transform.parent.gameObject);
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
            if (GameManager.Instance.m_CurrentWaitingCustomer == 1 && GameManager.Instance.m_AllGuestsVisitedToday >=
                GameManager.Instance.m_DailyMaxCustomer)
            {
                Debug.Log("Wave finish");
                return true;
            }
            else
            {
                Debug.Log("Wave not finish");
                return false;
            }

        }
    }

}
