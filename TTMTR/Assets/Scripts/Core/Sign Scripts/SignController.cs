using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour {

    // Use this for initialization
    private SignSpawn[] signMarkers;
    private List<Sign> possibleSigns;
    public List<Sign> defaultSigns;
    void Start() {
        if(possibleSigns == null)
        {
            possibleSigns = defaultSigns;
        }
        signMarkers = FindObjectsOfType(typeof(SignSpawn)) as SignSpawn[];
        foreach (SignSpawn sign in signMarkers)
        {
            sign.spawnSign(possibleSigns[Random.Range(0, possibleSigns.Count)]);
        }
    }

    public void setSigns(List<Sign> signs)
    {
        possibleSigns = signs;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
