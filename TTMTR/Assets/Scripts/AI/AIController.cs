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
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(player.transform.position, gameObject.transform.position) < playerVisualDistance) {
			curDest = player.transform.position;
		} else {
			if (Vector3.Distance(curDest, gameObject.transform.position) < 0.5f) {
				curDest = wanderPoints[Random.Range(0, wanderPoints.Count)].position;
			}
		}
		navAgent.destination = curDest;
	}

	public void Detect(Vector3 position) {
		curDest = position;
	}
}
