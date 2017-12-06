using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoController : MonoBehaviour, Detector, HasDifficulty {

	public UnityEngine.AI.NavMeshAgent navAgent;

	public List<Transform> wanderPoints;

	public GameObject mesh;

	bool detectedPlayer = false;
	bool detectedPlayerRecently = false;

	public float timeToForgetPlayer = 10.0f;
	public float timeSincePlayerSeen = 10.0f;

	public List<AudioSource> groundSources;
	public List<AudioSource> treeSources;

	bool inTrees = false;

	[SerializeField]
	private float maxSoundTimer = 0.25f;
	private float curSoundTimer;

	private float maxTreeSwitchTimer = 10.0f;
	private float curTreeSwitchTimer;

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
		curSoundTimer = maxSoundTimer;
		curTreeSwitchTimer = maxTreeSwitchTimer;
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
		if (detectedPlayerRecently) {
			timeSincePlayerSeen -= Time.deltaTime;
			if (timeSincePlayerSeen <= 0.0f) {
				detectedPlayerRecently = false;
				timeSincePlayerSeen = 10.0f;
			}
		}
		if (curSoundTimer <= 0.0f) {
			makeMovementSound();
			curSoundTimer = maxSoundTimer;
		} else {
			curSoundTimer -= Time.deltaTime;
		}
		if (curTreeSwitchTimer <= 0.0f) {
			toggleInTrees ();
			curTreeSwitchTimer = maxTreeSwitchTimer;
		} else {
			curTreeSwitchTimer -= Time.deltaTime;
		}
		navAgent.destination = curDest;
	}

	public void Detect(DetectionObject detected) {
		Debug.Log("Something detected at " + detected.position.ToString());
		if (!detectedPlayer) {
			detectedPlayer = true;
			detectedPlayerRecently = true;
		} else if (!detectedPlayerRecently) {
			detectedPlayerRecently = true;
		}

		if (detected.CompareTo (curDetectionObject) <= 0) {
			curDetectionObject = detected;
			curDest = detected.position;
		} else {
			detectedObjects.Push (detected);
		}
	}

	void toggleInTrees() {
		inTrees = !inTrees;
		if (inTrees)
			mesh.transform.position = new Vector3(mesh.transform.position.x, 10, mesh.transform.position.z);
		else
			mesh.transform.position = new Vector3(mesh.transform.position.x, 1, mesh.transform.position.z);
	}

	void makeMovementSound() {
		AudioSource chosen;
		if (inTrees) {
			chosen = (AudioSource)treeSources [Random.Range (0, treeSources.Count)];
		} else {
			chosen = (AudioSource)groundSources [Random.Range (0, groundSources.Count)];
		}
		if(chosen != null && !chosen.isPlaying) chosen.Play ();
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
