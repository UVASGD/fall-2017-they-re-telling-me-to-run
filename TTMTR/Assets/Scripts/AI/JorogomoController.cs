﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JorogomoController : MonoBehaviour, Detector {

	public UnityEngine.AI.NavMeshAgent navAgent;

	public List<Transform> wanderPoints;

	Vector3 curDest;

	public Vision eyes;
	public Smell nose;

	PriorityQueue<DetectionObject> detectedObjects;
	DetectionObject curDetectionObject;

	// Use this for initialization
	void Start () {
		curDest = gameObject.transform.position;
		detectedObjects = new PriorityQueue<DetectionObject> ();
		SoundReceiver rec = GetComponent<SoundReceiver>();
		if (rec != null) {
			rec.SetDetector(this);
		}
		if (eyes != null) {
			eyes.SetDetector(this);
		}
		if (nose != null) {
			nose.SetDetector(this);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(curDest, gameObject.transform.position) < 1.5f) {
			Debug.Log("Destination Reached");
			if (detectedObjects.Length > 0) {
				curDest = detectedObjects.Pop().position;
			} else {
				curDest = wanderPoints [Random.Range (0, wanderPoints.Count)].position;
			}
		}
		navAgent.destination = curDest;
	}

	public void Detect(DetectionObject detected) {
		Debug.Log("Something detected at " + detected.position.ToString());
		if (detected.CompareTo (curDetectionObject) <= 0) {
			curDetectionObject = detected;
			curDest = detected.position;
		} else {
			detectedObjects.Push (detected);
		}
	}

	void LookAround() {

	}
}
