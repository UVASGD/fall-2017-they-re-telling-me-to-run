using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectionObject : IComparable<DetectionObject> {

	Vector3 position;
	string type;
	float intensity;

	public int CompareTo(DetectionObject d) {
		return -1;
	}
}
