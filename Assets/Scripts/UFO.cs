using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{

    [Header("Physics")]
    public Rigidbody UFORigidbody;

    public GameObject Earth;

    [Tooltip("Speed when moving forward, in units/second")]
    [Range(0, 100)]
    public float Speed = 50;

    [Tooltip("Max number of passengers that you can carry at the same time")]
    [Range(0, 100)]
    public int maxPassengers;
    public int passengers;

    public float UFORadious;

    [Tooltip("Shooting speed of the humans")]
    [Range(0, 500)]
    public float ShootingSpeed;

    public GameObject ShootedHuman;

    public GameObject Cannon;

    public Transform SpawnPoint;

    // Use this for initialization
    void Start()
    {
        UFORigidbody = GetComponent<Rigidbody>();
        Earth = GameObject.FindWithTag("Earth");

    }

    void Update()
    {
        //Check if shooting
        Shoot();
        //Check if close enought to earth to reload
        ReloadHumans();
    }

    //Update used for physics. User controlls
    private void FixedUpdate()
    {
        /* If the vertical axis is not zero,
         *  we move the spaceship.
         */
        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0)
        {
            /* We add force in the "up" direction,
             * which points in the direction the spaceship is 
               pointing at.
             */
            UFORigidbody.AddForce(transform.forward * vertical * Speed);
        }

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            UFORigidbody.AddForce(transform.right * horizontal * Speed);
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, 1 << 10))
        {
            Vector3 point = new Vector3(hit.point.x, Cannon.transform.position.y, hit.point.z);
            Cannon.transform.LookAt(point);
        }
    }

    //Shoot mechanics
    private void Shoot()
    {
        if ( Input.GetMouseButtonDown (0)) 
        {
            if (passengers > 0)
            {
                GameObject shootedPassenger = Instantiate(ShootedHuman, SpawnPoint.position, SpawnPoint.rotation);
                Rigidbody passengerRigidbody = shootedPassenger.GetComponent<Rigidbody>();
                Rigidbody rb = GetComponent<Rigidbody>();
                Vector3 CurrentVelocity = rb.velocity;
                print(CurrentVelocity);
                passengerRigidbody.AddForce((SpawnPoint.forward) * ShootingSpeed, ForceMode.Impulse);
                passengers--;
                rb.AddForce((-SpawnPoint.forward) * (ShootingSpeed/2), ForceMode.Impulse);
            }

        }
    }

    public void EjectPassengers()
    {
        for (int i = 0; i < passengers; i++)
        {
            Vector3 RandomDirection = (new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)) - transform.position).normalized;
            GameObject shootedPassenger = Instantiate(ShootedHuman, transform.position + (RandomDirection * UFORadious), transform.rotation);
            Rigidbody passengerRigidbody = shootedPassenger.GetComponent<Rigidbody>();
            passengerRigidbody.AddForce(RandomDirection * ShootingSpeed);
            
        }

        passengers = 0;
    }

    public void ReloadHumans()
    {
        if (Vector3.Distance(Earth.transform.position, transform.position) < 13)
        {
            passengers = maxPassengers;
        }
    }
}