using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	Collider collider;
	public bool active = false;
	GameObject lure;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("trap: " + active);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.name == "lure") {
			lure = col.gameObject;
			activateTrap ();
		}
	}

	void activateTrap() {
		active = true;
	}

	public void deactivateTrap() {
		active = false;
		Destroy (lure);
	}
}
