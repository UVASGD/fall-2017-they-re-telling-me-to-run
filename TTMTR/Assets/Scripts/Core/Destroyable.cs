using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {
	public GameObject destroyer;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == destroyer.name) {
			Destroy (this.gameObject);
		}
	}
}
