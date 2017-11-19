using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDifficulty : MonoBehaviour {
	public float timekeep = 120; 

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		timekeep -= (Time.deltaTime);
		Debug.Log (timekeep);
		if (timekeep < 0) {
			timekeep = 120;
		}
	}
}
