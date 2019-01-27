using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {

	[SerializeField] public int maxPeople = 0;
	[SerializeField] public int currentPeople = 0;
	[SerializeField] GameObject human;
	private bool humanPresent;
    [SerializeField] long pointsPerHuman;
    //[SerializeField] Slider mySlider;
	[SerializeField] Text myText;

    public GameManager gManager;

    public GameObject OrbitCenter;

    [Tooltip("Orbitatng speed, in units/second")]
    [Range(0, 100)]
    public float Speed;

    void Awake(){
		//mySlider.maxValue = maxPeople;
		myText.text = "0" + "/" + maxPeople.ToString();
	}

    void Start()
    {
        OrbitCenter = GameObject.FindWithTag("Earth");
        gManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
		humanPresent = false;
		human.SetActive (false);

    }

    void Update()
    {
        transform.RotateAround(OrbitCenter.transform.position, Vector3.up, Speed * Time.deltaTime);
		if(currentPeople >= maxPeople && !humanPresent){
			human.SetActive (true);
			humanPresent = true;
		}
    }

	void OnCollisionEnter(Collision other){

		if (other.gameObject.tag == "Human" && currentPeople < maxPeople) {
            //Add score to the Game manager
            gManager.Score += pointsPerHuman;

            //Take the people out from earth population (We dont want them to just shoot peolpe into space, so they only get the reward if people land on a new planet)
            gManager.NumberOfHumans -= pointsPerHuman;

            //Handle the maximum people on the planet stuff
            currentPeople++;
			UpdatePeopleCounter ();
			Destroy (other.gameObject);
			SpawnHuman ();
		}
	}

	void UpdatePeopleCounter(){
		//mySlider.value = currentPeople;
		myText.text = currentPeople.ToString() + "/" + maxPeople.ToString();
	}

	void SpawnHuman(){
		//Make people appear on the surface of the planet
	}


}
