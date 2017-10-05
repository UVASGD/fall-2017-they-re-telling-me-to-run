using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
	
	public UnityEngine.AI.NavMeshAgent navAgent;

	public GameObject player;

	public float playerVisualDistance = 10.0f;

	public List<Transform> wanderPoints;

	Transform curDest;

	// Use this for initialization
	void Start () {
		curDest = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(player.transform.position, gameObject.transform.position) < playerVisualDistance) {
			curDest = player.transform;
		} else {
			if (Vector3.Distance(curDest.position, gameObject.transform.position) < 0.5f) {
				curDest = wanderPoints[Random.Range(0, wanderPoints.Count)];
			}
		}
		navAgent.destination = curDest.position;
	}
}
