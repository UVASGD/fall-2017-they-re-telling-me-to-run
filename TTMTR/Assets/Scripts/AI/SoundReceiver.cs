using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReceiver : MonoBehaviour {
	
	public float soundThreshold;

	public Detector detector;

	public virtual void Receive(float intensity, Vector3 position) {
		detector.Detect(position);
	}
}
