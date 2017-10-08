using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {

	public string targetTag;

	public GameObject agent;

	public Detector detector;

	// Use this for initialization
	void Start () {
		if (agent == null) {
			agent = gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider coll) {
		if (!coll.CompareTag(targetTag)) {
			return;
		}

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
			return;
		}

		detector.Detect(hit.point);
	}
}
