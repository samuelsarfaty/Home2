using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoorHuman : MonoBehaviour {

    public Rigidbody HumanRigidbody;

    public GameObject atractingPlanet;

    public float EarthGravity;

    public float PlanetGravity;

    public float gravity;

    private SphereCollider[] sCollider;

    private float radius;
	// Use this for initialization
	void Start () {
		HumanRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (atractingPlanet != null)
        {
            //check that they are still within the radious
            //Not implemented yet

            //Calculate direction to planet
            Vector3 directionToplanet = atractingPlanet.transform.position - transform.position;

            //Apply the force
            HumanRigidbody.AddForce(directionToplanet * gravity);

            //check that its still in orbit, otherwise ignore its gravity
            if (Vector3.Distance(transform.position, atractingPlanet.transform.position) > radius)
            {
                atractingPlanet = null;
            }
        }
	}

    //Check trigger enter of the planet gravity
    private void OnTriggerEnter(Collider other)
    {
        //if its a planet assign the radious
        if (other.tag == "Planet")
        {
            //make sure that they are only affected by gravity if the planet is not full
            Planet closePlanet = other.gameObject.GetComponent<Planet>();
            if (closePlanet.currentPeople < closePlanet.maxPeople)
            {
                atractingPlanet = other.gameObject;
                gravity = PlanetGravity;
                sCollider = other.gameObject.GetComponents<SphereCollider>();
                radius = Mathf.Max(sCollider[0].radius, sCollider[1].radius);
            }
        } else if (other.tag == "Earth")
        {
            atractingPlanet = other.gameObject;
            gravity = EarthGravity;
            sCollider = other.gameObject.GetComponents<SphereCollider>();
            radius = Mathf.Max(sCollider[0].radius, sCollider[1].radius);

            Debug.Log("Radius = " + radius);
        }

        //Get radious of attraction (with wrong scalated objects it doesnt work well, this fixes it)
        if(radius < 5)
        {
            radius = 13;
        }

    }
}
