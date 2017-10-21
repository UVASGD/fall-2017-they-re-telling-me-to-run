using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitOdor : MonoBehaviour {

	public float timeBetweenEmissions;

	public GameObject odor;

	float curTimer;

	// Use this for initialization
	void Start () {
		curTimer = timeBetweenEmissions;
	}
	
	// Update is called once per frame
	void Update () {
		if (curTimer <= 0.0f) {
			Emit();
			curTimer = timeBetweenEmissions;
		} else {
			curTimer -= Time.deltaTime;
		}
	}

	void Emit() {
		float randX = gameObject.transform.position.x + Random.Range(-1.0f, 1.0f);
		float randZ = gameObject.transform.position.z + Random.Range(-1.0f, 1.0f);

		Instantiate(odor, new Vector3(randX, gameObject.transform.position.y, randZ), Quaternion.identity);
	}
}
