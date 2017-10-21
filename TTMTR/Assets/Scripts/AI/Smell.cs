using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smell : Sensor {

	Dictionary<int, GameObject> odors;

	// Use this for initialization
	void Start () {
		odors = new Dictionary<int, GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void UpdateTarget() {
		Vector3 center = Vector3.zero;
		if (odors.Values.Count == 0) {
			return;
		}
		if (odors.Values.Count == 1) {
			Dictionary<int, GameObject>.ValueCollection vals = odors.Values;
			Dictionary<int, GameObject>.ValueCollection.Enumerator enumer = vals.GetEnumerator();
			enumer.MoveNext();
			GameObject go = enumer.Current;
			Vector3 pos = go.transform.position;
			Debug.Log("Single odor found at " + pos);
			detector.Detect(pos);
			return;
		}
		Bounds bounds = new Bounds();
		foreach (GameObject obj in odors.Values) {
			if (bounds.center == Vector3.zero) {
				bounds = new Bounds(obj.transform.position, Vector3.zero);
			} else {
				bounds.Encapsulate(obj.transform.position);
			}
		}
		Debug.Log("Found center at " + bounds.center);
		detector.Detect(bounds.center);
	}

	void OnTriggerEnter(Collider coll) {
		GameObject obj = coll.gameObject;
		if (!obj.CompareTag("Odor")) {
			return;
		}
		OdorParticle op;
		op = obj.GetComponent<OdorParticle>();
		if (op == null) {
			return;
		}

		odors.Add(obj.GetInstanceID(), obj);
		UpdateTarget();
	}

	void OnTriggerExit(Collider coll) {
		GameObject obj = coll.gameObject;
		if (!obj.CompareTag("Odor")) {
			return;
		}
		bool isRemoved = false;
		isRemoved = odors.Remove(obj.GetInstanceID());
		Destroy (obj);
		if (!isRemoved) return;
		UpdateTarget();
	}
}
