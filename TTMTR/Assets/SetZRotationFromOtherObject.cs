using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZRotationFromOtherObject : MonoBehaviour {

    public Transform otherObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.rotation = new Quaternion(
            this.gameObject.transform.rotation.x,
             otherObj.rotation.y,
             this.gameObject.transform.rotation.z,
             this.gameObject.transform.rotation.w);
	}
}
