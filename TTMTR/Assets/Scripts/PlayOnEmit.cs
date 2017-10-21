using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnEmit : MonoBehaviour {

	private ParticleSystem particleSys;
	//private AudioSource audSrc;

	private int count = 0;

	public List<AudioSource> audSrcs;

	// Use this for initialization
	void Start () {
		particleSys = this.gameObject.GetComponent<ParticleSystem> ();
		//audSrc = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (particleSys.particleCount > count) 
		{
			int index = Random.Range (-1, audSrcs.Count);
			if (index > 0) {
				AudioSource chosen = (AudioSource)audSrcs [index];
				if (!chosen.isPlaying)
					chosen.Play ();
				count++;
			}
		} 

		if(!particleSys.isEmitting)
		{
			count = 0;
		}
	}
}
