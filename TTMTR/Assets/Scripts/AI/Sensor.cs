using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensor : MonoBehaviour {
	public Detector detector;

	public void SetDetector(Detector detector) {
		this.detector = detector;
	}

}
