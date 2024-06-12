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
        public int m_WaitingPosition;
        public int m_OldWaitingPosition;

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
            StartCoroutine(WalkToCashDesk()); //Nur zum testen
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Destroy)
            {
                FinishOrder();
                m_Destroy = false;
            }
        }

        public void StartWalk()
        {
            StartCoroutine(WalkToCashDesk());
        }

        public IEnumerator WalkToCashDesk()
        {
            m_Agent = GetComponent<NavMeshAgent>();

            if (m_Target != null)
            {
                m_Animator.SetBool("Walking", true);
                m_Agent.SetDestination(m_Target.position);
            }
            else
            {
                yield return null;
            }

            // Warte bis der Agent tatsächlich am Ziel angekommen ist
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

        public void FinishOrder()
        {
            GameManager.Instance.m_CustomersList.Remove(this);
            foreach (var customer in GameManager.Instance.m_CustomersList)
            {
                customer.m_Target.position = m_Target.position + new Vector3(0, 0, +2f);
                customer.StartWalk();
            }

            GameManager.Instance.m_CurrentWaitingCustomer -= 1;
            Destroy(gameObject);
        }

    }

}
