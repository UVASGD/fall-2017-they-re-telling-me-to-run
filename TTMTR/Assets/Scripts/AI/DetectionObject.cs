using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectionObject : IComparable<DetectionObject> {

	public enum DetectionType {
		Sight,
		Smell,
		Hearing
	}

	public Vector3 position;
	public DetectionType type;
	public float intensity;

	public DetectionObject(Vector3 pos, DetectionType t, float intense) {
		position = pos;
		type = t;
		intensity = intense;
	}

	public int CompareTo(DetectionObject d) {
		if (type == d.type) {
			return intensity.CompareTo (d.intensity);
		}
		if (type == DetectionType.Sight && d.type != DetectionType.Sight) {
			return -1;
		} else { 
			if (type == DetectionType.Hearing && d.type == DetectionType.Smell) {
				return -1;
			}
			return 1;
		}
	}
}
