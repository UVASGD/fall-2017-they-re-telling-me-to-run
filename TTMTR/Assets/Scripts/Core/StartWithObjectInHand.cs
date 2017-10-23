using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControllerGrabObjectAndTeleport))]
public class StartWithObjectInHand : MonoBehaviour {

    public GameObject itemPrefab;

    public GameObject item;

	// Use this for initialization
	void Start () {
        item = Instantiate(itemPrefab);
        var vec3 = this.gameObject.transform.position;
        item.transform.position = new Vector3(vec3.x, vec3.y, vec3.z); 
        GetComponent<ControllerGrabObjectAndTeleport>().collidingObject = item;
        GetComponent<ControllerGrabObjectAndTeleport>().GrabObject();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
