using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JorogomoController : MonoBehaviour, Detector {

	public UnityEngine.AI.NavMeshAgent navAgent;

	public List<Transform> wanderPoints;

	bool detectedPlayer = false;
	bool detectedPlayerRecently = false;

	public float timeToForgetPlayer = 10.0f;
	public float timeSincePlayerSeen = 10.0f;

	public List<AudioSource> singSources;
	public List<AudioSource> crawlieSources;

	[SerializeField]
	private float maxCrawlieTimer = 0.25f;
	private float curCrawlieTimer;

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
		curCrawlieTimer = maxCrawlieTimer;
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
		if (detectedPlayerRecently) {
			timeSincePlayerSeen -= Time.deltaTime;
			if (timeSincePlayerSeen <= 0.0f) {
				detectedPlayerRecently = false;
				timeSincePlayerSeen = 10.0f;
			}
		}
		if (curCrawlieTimer <= 0.0f) {
			Crawlie();
			curCrawlieTimer = maxCrawlieTimer;
		} else {
			curCrawlieTimer -= Time.deltaTime;
		}
		navAgent.destination = curDest;
	}

	public void Detect(DetectionObject detected) {
		Debug.Log("Something detected at " + detected.position.ToString());
		if (!detectedPlayer) {
			detectedPlayer = true;
			detectedPlayerRecently = true;
			Sing();
		} else if (!detectedPlayerRecently) {
			detectedPlayerRecently = true;
			Sing();
		}

		if (detected.CompareTo (curDetectionObject) <= 0) {
			curDetectionObject = detected;
			curDest = detected.position;
		} else {
			detectedObjects.Push (detected);
		}
	}

	void LookAround() {

	}

	void Sing() {
		AudioSource chosen = (AudioSource)singSources[Random.Range(0, singSources.Count)];
		if(chosen != null && !chosen.isPlaying) chosen.Play ();
	}

	void Crawlie() {
		AudioSource chosen = (AudioSource)crawlieSources[Random.Range(0, crawlieSources.Count)];
		if(chosen != null && !chosen.isPlaying) chosen.Play ();
	}
}
