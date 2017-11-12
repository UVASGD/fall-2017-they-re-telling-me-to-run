using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour {

	public float maxTimer = 120.0f;
	float curTimer;

	private int curDifficulty;

	public int CurrentDifficulty {
		get {
			return curDifficulty;
		}

		private set {
			curDifficulty = value;
		}
	}

	public List<HasDifficulty> diffObjects;

	void Init (float maxTimer) {
		this.maxTimer = maxTimer;
	}
	// Use this for initialization
	void Start () {
		curTimer = maxTimer;

		if (diffObjects == null) {
			diffObjects = new List<HasDifficulty> ();
		}

		curDifficulty = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (curTimer <= 0.0f) {
			IncDifficulty ();
		} else {
			curTimer -= Time.deltaTime;
		}
	}

	public void IncDifficulty() {
		curTimer = maxTimer;
		foreach (HasDifficulty diff in diffObjects) {
			diff.DifficultyUp ();
		}
		curDifficulty++;
	}

	public void DecDifficulty() {
		foreach (HasDifficulty diff in diffObjects) {
			diff.DifficultyDown ();
		}
		curDifficulty--;
	}

	public void AddDiffObject(HasDifficulty diff) {
		diffObjects.Add (diff);
	}
}
