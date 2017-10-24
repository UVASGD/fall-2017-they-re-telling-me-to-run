using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JorogomoController : MonoBehaviour, Detector, HasDifficulty {

	public UnityEngine.AI.NavMeshAgent navAgent;

	public List<Transform> wanderPoints;

	Vector3 curDest;

	public Vision eyes;
	public Smell nose;
	SoundReceiver rec;

	PriorityQueue<DetectionObject> detectedObjects;
	DetectionObject curDetectionObject;

	public GameController gameCont;

	[SerializeField]
	private Settings settings;

	// Use this for initialization
	void Start () {
		curDest = gameObject.transform.position;
		detectedObjects = new PriorityQueue<DetectionObject> ();
		rec = GetComponent<SoundReceiver>();
		if (rec != null) {
			rec.SetDetector(this);
			rec.SetThreshold (settings.incThreshold);
		}
		if (eyes != null) {
			eyes.SetDetector(this);
			eyes.gameObject.transform.localScale = settings.incEyeScale;
		}
		if (nose != null) {
			nose.SetDetector(this);
			nose.gameObject.transform.localScale = settings.incNoseScale;
		}
		navAgent.speed = settings.initSpeed;

		gameCont.RegisterDifficultyObservation (this);
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

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			DifficultyUp ();
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			DifficultyDown ();
		}
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

	public void DifficultyUp() {
		navAgent.speed = navAgent.speed + settings.incSpeed;
		rec.SetThreshold (rec.soundThreshold + settings.incThreshold);
		eyes.gameObject.transform.localScale = eyes.gameObject.transform.localScale + settings.incEyeScale;
		nose.gameObject.transform.localScale = nose.gameObject.transform.localScale + settings.incNoseScale;
	}

	public void DifficultyDown() {
		navAgent.speed = navAgent.speed - settings.incSpeed;
		rec.SetThreshold (rec.soundThreshold - settings.incThreshold);
		eyes.gameObject.transform.localScale = eyes.gameObject.transform.localScale - settings.incEyeScale;
		nose.gameObject.transform.localScale = nose.gameObject.transform.localScale - settings.incNoseScale;
	}

	[System.Serializable]
	public class Settings {
		public float initSpeed = 5.0f;
		public float incSpeed = 5.0f;

		public float initThreshold = 0.0f;
		public float incThreshold = -2.0f;

		public Vector3 initEyeScale = Vector3.one;
		public Vector3 incEyeScale = Vector3.one;

		public Vector3 initNoseScale = Vector3.one;
		public Vector3 incNoseScale = Vector3.one;
	}
}
