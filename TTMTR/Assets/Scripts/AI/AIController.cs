using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour, Detector {
	
	public UnityEngine.AI.NavMeshAgent navAgent;

	public GameObject player;

	public float playerVisualDistance = 10.0f;

	public List<Transform> wanderPoints;

	Vector3 curDest;

	// Use this for initialization
	void Start () {
		curDest = gameObject.transform.position;
		SoundReceiver rec = GetComponent<SoundReceiver>();
		if (rec != null) {
			rec.SetDetector(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(curDest, gameObject.transform.position) < 1.5f) {
			Debug.Log("Destination Reached");
			curDest = wanderPoints[Random.Range(0, wanderPoints.Count)].position;
		}
		navAgent.destination = curDest;
	}

	public void Detect(DetectionObject detection) {
		Debug.Log("Something detected at " + detection.position.ToString());
		curDest = detection.position;
	}
}
