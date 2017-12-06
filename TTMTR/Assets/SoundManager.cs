using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public GameObject MMAudioHolder;
    public GameObject CaveAudioHolder;
    public GameObject ForestAudioHolder;

    public float FADE_DEC_PER_S;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FadeMMforCave()
    {
        StartCoroutine(FadeAudioOut(MMAudioHolder.GetComponent<AudioSource>()));
        foreach (AudioSource aud in CaveAudioHolder.GetComponentsInChildren<AudioSource>())
        {
            aud.Play();
            StartCoroutine(FadeAudioIn(aud));
        }
    }

    public void FadeMMforForest()
    {
        StartCoroutine(FadeAudioOut(MMAudioHolder.GetComponent<AudioSource>()));
        foreach (AudioSource aud in ForestAudioHolder.GetComponentsInChildren<AudioSource>())
        {
            aud.Play();
            StartCoroutine(FadeAudioIn(aud));
        }
    }

    IEnumerator FadeAudioOut(AudioSource toFade)
    {
        while (toFade.volume > 0)
        {
            toFade.volume -= FADE_DEC_PER_S * Time.deltaTime;
            yield return null;
        }
        toFade.Stop();
    }

    IEnumerator FadeAudioIn(AudioSource toFade)
    {
        while (toFade.volume < 1)
        {
            toFade.volume += FADE_DEC_PER_S * Time.deltaTime;
            yield return null;
        }
    }
}
