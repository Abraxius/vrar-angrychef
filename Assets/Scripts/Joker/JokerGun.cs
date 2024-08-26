using System;
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
    private AudioSource audioSource;

    [SerializeField] AudioClip firesound;
    [SerializeField] AudioClip stucksound;
    //logic variables
    private float lastUsedTime;

    //Extra objects
    public GameObject canvas;
    public AmmoDisplay display;

    [SerializeField] private bool debugFunction;

    private GameInput _gameInput;

    private void Awake()
    {
        _gameInput = new GameInput();
    }

    private void OnEnable()
    {
        _gameInput.Player.Enable();
    }

    private void OnDisable()
    {
        _gameInput.Player.Disable();
    }
    
    private void Update()
    {
        if (debugFunction)
        {
            if (_gameInput.Player.Interact.triggered)
            {
                Fire();
                Debug.Log("E is pressed");
            }
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject. Please add one.");
        }
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
        if (audioSource != null && firesound != null)
        {
            audioSource.PlayOneShot(stucksound);
        }
    }
    public void Fire()
    {
        GameObject newObject = Instantiate(Projectile, StartingPoint.position, StartingPoint.rotation, null);
        if (newObject.transform.GetChild(0).TryGetComponent(out Rigidbody rigidBody))
        {
            ApplyForce(rigidBody);
        }
        Instantiate(Particles, StartingPoint.position, StartingPoint.rotation, null);

        if (audioSource != null && firesound != null)
        {
            audioSource.PlayOneShot(firesound);
        }

        GameManager.Instance.m_Ammunition-= 1;
        display.UpdateCanvas();
    }

    void ApplyForce(Rigidbody rigidBody)
    {
        Vector3 force = StartingPoint.up * Strength;
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
