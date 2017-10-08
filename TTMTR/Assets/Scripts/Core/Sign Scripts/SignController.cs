using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour {

    // Use this for initialization
    private SignSpawn[] signMarkers;
    public List<GameObject> possibleSigns;
	void Start () {
        signMarkers = FindObjectsOfType(typeof(SignSpawn)) as SignSpawn[];
        foreach(SignSpawn sign in signMarkers)
        {
            if(sign.prefab == null) sign.prefab = possibleSigns[Random.Range(0,possibleSigns.Count)];
            sign.spawnSign();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
