using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public GameObject OrbitCenter;

    [Tooltip("Orbitatng speed, in units/second")]
    [Range(0, 100)]
    public float Speed = 50;

    public float HitStrength;

    private bool applyForce = false;

    private Rigidbody hitObject;
    // Use this for initialization
    void Start()
    {
        OrbitCenter = GameObject.FindWithTag("Earth");
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate function
        transform.RotateAround(OrbitCenter.transform.position, Vector3.up, Speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        hitObject = collision.gameObject.GetComponent<Rigidbody>();

        //if the hit object has a rigidbody
        if (hitObject != null)
        {
            applyForce = true;
        }
    }

    private void FixedUpdate()
    {
        if (applyForce)
        {
            Debug.Log("Force Applied");
            Vector3 directionHit = hitObject.gameObject.transform.position - transform.position;
            hitObject.AddForce(directionHit * HitStrength);
            applyForce = false;

            //make it expell all the passengers
            if (hitObject.gameObject.tag == "UFO")
            {
                UFO ufoObject = hitObject.gameObject.GetComponent<UFO>();
                ufoObject.EjectPassengers();
            }
        }
    }

}
