using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReceiver : Sensor {
	
	public float soundThreshold;

	public virtual void Receive(float intensity, Vector3 position) {
		detector.Detect(position);
	}
}
