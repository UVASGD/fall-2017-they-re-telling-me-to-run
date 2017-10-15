using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnEmit : MonoBehaviour {

	private ParticleSystem particleSys;
	private AudioSource audSrc;

	private int count = 0;

	// Use this for initialization
	void Start () {
		particleSys = this.gameObject.GetComponent<ParticleSystem> ();
		audSrc = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (particleSys.particleCount > count) 
		{
			if(!audSrc.isPlaying) audSrc.Play ();
			count++;
		} 

		if(!particleSys.isEmitting)
		{
			count = 0;
		}
	}
}
