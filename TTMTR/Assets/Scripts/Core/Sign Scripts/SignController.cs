using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour {

    // Use this for initialization
    public Sign[] signMarkers;
    public List<GameObject> possibleSigns;
	void Awake () {
        foreach(Sign sign in signMarkers)
        {
            sign.prefabs = possibleSigns;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
