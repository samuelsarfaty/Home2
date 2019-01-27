using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	Transform earth;

	void Awake(){
		earth = GameObject.FindWithTag ("Earth").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (earth);
	}
}
