using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactiveAfterTime : MonoBehaviour {

    public float timeToWait;
    public float timer;
    bool done = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!done)
        {
            if (timer >= timeToWait)
            {
                this.gameObject.SetActive(false);
                done = true;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
