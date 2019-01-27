using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalCanvas : MonoBehaviour {

	[SerializeField] Slider populationSlider;
	[SerializeField] Text scoreText;


	private GameManager gm;


	void Awake(){
		gm = GameObject.FindObjectOfType<GameManager> ();
		populationSlider.maxValue = gm.MaxNHumans;

	}

	void Start(){
		populationSlider.value = gm.StartNumberOfHumans;
	}

	void Update(){
		scoreText.text = gm.Score.ToString ();
		populationSlider.value = gm.NumberOfHumans;
	}
}
