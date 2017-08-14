using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialLayoutGroup : MonoBehaviour {

    public float yOffset;
    [SerializeField]
    private float rotation;

    private float timerOut = 0f;
    private float timerIn = 0f;
    private float timeToLerp = 1f;

	// Use this for initialization
	void Start () {
        rotation = 360 / transform.childCount;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Rotate(0,0, rotation * i);
            //transform.GetChild(i).Translate(new Vector3(0, yOffset, 0));
        }
    }

    public void TurnOn()
    {   
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = Vector3.zero;
        }
        this.gameObject.SetActive(true);
        timerOut = 0;
        //StopCoroutine(FanIn());
        StopAllCoroutines();
        StartCoroutine(FanOut());
    }

    public void TurnOff()
    {
        timerIn = 0;
        //StopCoroutine(FanOut());
        StopAllCoroutines();
        StartCoroutine(FanIn());
    }

    private IEnumerator FanOut()
    {
        while (timerOut < timeToLerp)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).Translate(new Vector3(0, yOffset * Time.unscaledDeltaTime, 0));
            }
            timerOut += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private IEnumerator FanIn()
    {
        while (timerIn < timeToLerp)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).Translate(new Vector3(0, -1 * yOffset * Time.unscaledDeltaTime, 0));
            }
            timerIn += Time.unscaledDeltaTime;
            yield return null;
        }
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

   
}
