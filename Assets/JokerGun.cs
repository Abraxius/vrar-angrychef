using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerGun : MonoBehaviour
{
    //Difference between Weapons
    [SerializeField]
    Transform StartingPoint;
    public int Ammo;
    public GameObject Projectile;
    [SerializeField] float Strength;
    [SerializeField] float FireRate;

    //logic variables
    private float lastUsedTime;

    void Start()
    {
        
    }
    
    public void PullTrigger()
    {
        if(Time.time > lastUsedTime + FireRate && Ammo > 0)
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

        if (newObject.TryGetComponent(out Rigidbody rigidBody))
            ApplyForce(rigidBody);

        Ammo -= 1;

    }

    void ApplyForce(Rigidbody rigidBody)
    {
        Vector3 force = StartingPoint.forward * Strength;
        rigidBody.AddForce(force);
    }
}
