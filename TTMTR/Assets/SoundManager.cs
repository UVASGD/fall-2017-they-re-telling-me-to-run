using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    GameObject MMAudioHolder;
    GameObject CaveAudioHolder;
    GameObject ForestAudioHolder;

    public float FADE_DEC_PER_S;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FadeAudioOut(AudioSource toFade)
    {
        if (toFade.volume > 0)
        {
            toFade.volume -= FADE_DEC_PER_S * Time.deltaTime;
            yield return null;
        }
        toFade.Stop();
    }
}
