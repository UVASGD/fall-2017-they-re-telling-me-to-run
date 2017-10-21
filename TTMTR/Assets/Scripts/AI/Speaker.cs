using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour {

	public SoundEmitter emitter;
	public float secPerEmit;

	bool emitting = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!emitting) {
			StartCoroutine(emission());
		}
	}

	IEnumerator emission() {
		emitting = true;
		for (float t = 0.0f; t < secPerEmit; t += Time.deltaTime) {
			yield return null;
		}
		emitter.Emit(10000000.0f);
		emitting = false;
	}
}
