using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionFromOtherObject : MonoBehaviour {

    public Transform otherObj;

    public Vector3 offset;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = otherObj.transform.position + offset;
	}
}
