using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerGun : MonoBehaviour
{
    //Variables to make the weapon feel diferent
    [SerializeField] Transform StartingPoint;
    public GameObject Projectile;
    [SerializeField] float Strength;
    [SerializeField] float FireRate;
    [SerializeField] GameObject Particles;

    //logic variables
    private float lastUsedTime;
    private Vector3 shootingDirection = Vector3.forward;

    //Extra objects
    public GameObject canvas;
    public AmmoDisplay display;

    void Start()
    {
        
    }
    
    public void PullTrigger()
    {

        if(Time.time > lastUsedTime + FireRate && GameManager.Instance.m_Ammunition > 0)
        {
            Fire();
            lastUsedTime = Time.time;
        }
        else
        {
            Stuck();
        }
    }

    public void Stuck()
    {
        //play stuck sound  
    }
    public void Fire()
    {
        GameObject newObject = Instantiate(Projectile, StartingPoint.position, StartingPoint.rotation, null);
        Instantiate(Particles, StartingPoint.position, StartingPoint.rotation, null);

        if (newObject.TryGetComponent(out Rigidbody rigidBody))
        {
            ApplyForce(rigidBody);
        }

        GameManager.Instance.m_Ammunition-= 1;
        display.UpdateCanvas();
    }

    void ApplyForce(Rigidbody rigidBody)
    {
        Vector3 force = StartingPoint.forward * Strength;
        rigidBody.AddForce(force);
    }

    public void ActivateCanvas()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }
    public void DeactivateCanvas()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }
}
