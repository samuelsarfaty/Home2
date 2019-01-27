using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {

	[SerializeField] UFO parent;
	private Text myText;

	void Awake(){
		myText = GetComponent<Text> ();
	}

	void Update(){
		myText.text = parent.passengers.ToString ();
	}
}
