using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {
	public GameObject destroyer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}


	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == destroyer.name)
		{
			Destroy (this.gameObject);
		}
	}
}
