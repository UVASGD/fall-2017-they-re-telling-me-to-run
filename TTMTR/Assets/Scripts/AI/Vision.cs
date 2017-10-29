using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : Sensor {

	public string targetTag;

	public GameObject agent;

	// Use this for initialization
	void Start () {
		if (agent == null) {
			agent = gameObject;
		}
	}

	void OnTriggerStay(Collider coll) {
		if (!coll.CompareTag(targetTag)) {
			return;
		}
		Debug.Log("Found player");

		GameObject tar = coll.gameObject;
		Vector3 agentPos = agent.transform.position;
		Vector3 tarPos = tar.transform.position;
		Vector3 dir = tarPos - agentPos;

		float length = dir.magnitude;

		dir.Normalize();

		Ray ray = new Ray(agentPos, dir);

		RaycastHit hit;
		if (!Physics.Raycast(ray, out hit, length)) {
			return;
		}

		if (!hit.collider.gameObject.CompareTag(targetTag)) {
			Debug.Log(hit.collider.gameObject.tag);
			return;
		}

		detector.Detect(new DetectionObject(hit.point, DetectionObject.DetectionType.Sight, hit.distance));
	}
}
