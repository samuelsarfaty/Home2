using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerTrigger : MonoBehaviour {

	private MeshRenderer earthRenderer;
	public GameObject child;
	bool childActive;

	void Awake(){
		earthRenderer = GameObject.FindWithTag ("Earth").GetComponentInChildren<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(earthRenderer.isVisible && childActive){
			HideArrow ();
		} else if (!earthRenderer.isVisible && !childActive){
			ShowArrow ();
		}
	}

	void HideArrow(){
		childActive = false;
		child.SetActive (false);
	}

	void ShowArrow(){
		childActive = true;
		child.SetActive (true);
	}
}
