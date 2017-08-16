using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialLayoutGroup : MonoBehaviour {

    public enum ViewState
    {
        fanin,
        inning,
        fanout,
        outing
    }
    public ViewState state = ViewState.fanin;

    public float yOffset;
    public float scale;
    [SerializeField]
    private float rotation;

    private float timerOut = 0f;
    private float timerIn = 0f;
    private float timeToLerp = 0.3f;

	// Use this for initialization
	void Start () {
        rotation = 360 / transform.childCount;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Rotate(0,0, rotation * i);
            transform.GetChild(i).localScale = Vector3.zero;
            //transform.GetChild(i).Translate(new Vector3(0, yOffset, 0));
        }
    }

    public void TurnOn()
    {   
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = Vector3.zero;
            transform.GetChild(i).localScale = Vector3.zero;
        }
        this.gameObject.SetActive(true);
        timerOut = 0;
        state = ViewState.outing;
        StopAllCoroutines();
        StartCoroutine(FanOut());
    }

    public void TurnOff()
    {
        timerIn = 0;
        state = ViewState.inning;
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
                transform.GetChild(i).localScale = new Vector3(scale * (timerOut/timeToLerp), scale * (timerOut / timeToLerp), 0);
            }
            timerOut += Time.unscaledDeltaTime;
            yield return null;
        }
        state = ViewState.fanout;
    }

    private IEnumerator FanIn()
    {
        while (timerIn < timeToLerp)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).Translate(new Vector3(0, -1 * yOffset * Time.unscaledDeltaTime, 0));
                transform.GetChild(i).localScale = new Vector3(scale * (1-(timerIn / timeToLerp)), scale * (1-(timerIn / timeToLerp)), 0);
            }
            timerIn += Time.unscaledDeltaTime;
            yield return null;
        }
        this.gameObject.SetActive(false);
        state = ViewState.fanin;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Highlight(float angle)
    {
        float angleDelim = 360 / transform.childCount;
	float offset = angleDelim / 2;
	float angleIter = offset;
        int index = 0;
	while (angleIter < angle){
		index++;
		angleIter+= angleDelim;
	}
	index = index % transform.childCount;
	    
        Debug.Log(index);
        for(int i = 0; i < transform.childCount; i++)
        {
            if(i == index)
            {
                transform.GetChild(i).GetComponent<Image>().color = Color.red;
            }
            else
            {
                transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }
    }
}
