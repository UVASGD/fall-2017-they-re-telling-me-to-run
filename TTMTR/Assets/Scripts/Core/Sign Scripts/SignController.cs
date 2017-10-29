using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour {

    // Use this for initialization
    private SignSpawn[] signMarkers;
    public List<Sign> possibleSigns;
	void Start () {
        signMarkers = FindObjectsOfType(typeof(SignSpawn)) as SignSpawn[];
        foreach(SignSpawn sign in signMarkers)
        {   
            sign.spawnSign(possibleSigns[Random.Range(0, possibleSigns.Count)]);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
