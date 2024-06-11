using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerGun : MonoBehaviour
{
    //Difference between Weapons
    [SerializeField] Transform StartingPoint;
    public int Ammo;
    public GameObject Projectile;
    [SerializeField] float Strength;
    [SerializeField] float FireRate;
    [SerializeField] GameObject Particles;

    //logic variables
    private float lastUsedTime;
    private Vector3 shootingDirection = Vector3.forward;

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
        Quaternion rotation = Quaternion.identity;
        GameObject newObject = Instantiate(Projectile, StartingPoint.position, Quaternion.LookRotation(shootingDirection), null);
        GameObject shootingParticles = Instantiate(Particles, StartingPoint.position, StartingPoint.rotation, null);

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
