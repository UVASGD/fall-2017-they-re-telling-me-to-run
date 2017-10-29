using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour {

	public float soundAttenuation;

	public GameObject emitter;

	public Dictionary<int, SoundReceiver> receivers;

	void Start() {
		receivers = new Dictionary<int, SoundReceiver>();

		if (emitter == null) {
			emitter = gameObject;
		}
	}

	void OnTriggerEnter(Collider coll) {
		SoundReceiver rec = coll.gameObject.GetComponent<SoundReceiver>();
		if (rec == null) {
			return;
		}
		receivers.Add(coll.gameObject.GetInstanceID(), rec);
	}

	void OnTriggerExit(Collider coll) {
		SoundReceiver rec = coll.gameObject.GetComponent<SoundReceiver>();
		if (rec == null) {
			return;
		}
		receivers.Remove(coll.gameObject.GetInstanceID());
	}

	public void Emit(float intensity) {
		Debug.Log("Emitting sound of intensity: " + intensity);
		GameObject srObj;
		Vector3 srPos;

		float distance;

		float attenuated;

		Vector3 emitPos = emitter.transform.position;

		foreach (SoundReceiver rec in receivers.Values) {
			Debug.Log (rec.gameObject.name);
			srObj = rec.gameObject;
			srPos = srObj.transform.position;

			distance = Vector3.Distance(emitPos, srPos);

			attenuated = intensity - (soundAttenuation * distance);

			if (attenuated < rec.soundThreshold) continue;

			rec.Receive(attenuated, emitPos);
		}
	}
}
