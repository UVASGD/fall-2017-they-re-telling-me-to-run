using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

	int maxHealth = 2000;
	int currentHealth = 2000;
	GameObject monster;
	public GameObject camera;

	// Use this for initialization
	void Start () {
		monster = GameObject.FindGameObjectWithTag ("Monster");
		// right now just manually attaching camera to this script, not sure
		// if there is a better way to just grab it in start()
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(camera.transform.position,monster.transform.position);
		if (distance < 10) {
			takeDamage (5000);
		}
	}

	// Subtracts health from currentHealth, 
	// returns new currentHealth. If amount 
	// would bring currentHealth to <= 0,
	// sets currentHealth to 0 and initiates
	// failure condition in GameController
	int takeDamage(int amount) {
		if (amount < currentHealth) {
			currentHealth = currentHealth - amount;
		} else {
			currentHealth = 0;
			this.GetComponent<GameController>().failure();
		}
		return currentHealth;
	}
}
