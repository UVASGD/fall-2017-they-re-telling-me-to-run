using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

	public Transform goal;

	public UnityEngine.AI.NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		navAgent.destination = goal.position;
	}
}
