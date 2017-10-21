using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnPressKey : MonoBehaviour {

	public List<AudioSource> audSrcs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			AudioSource chosen = (AudioSource)audSrcs[Random.Range(0, audSrcs.Count)];
			if(!chosen.isPlaying) chosen.Play ();
		}
	}
}
