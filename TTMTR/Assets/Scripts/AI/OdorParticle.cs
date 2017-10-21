using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdorParticle : MonoBehaviour {

	public float timespan;
	float timer;

	// Use this for initialization
	void Start () {
		if (timespan < 0.0f) {
			timespan = 0.0f;
		}
		timer = timespan;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0.0f) {
			Destroy(this.gameObject);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, 0.5f);
	}


}
