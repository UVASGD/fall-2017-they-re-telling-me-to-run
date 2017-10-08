using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Sign : MonoBehaviour {

    public GameObject prefab;
	// Use this for initialization
	void Start () {
        GameObject sign = GameObject.Instantiate(prefab, this.transform);
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Handles.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up);
        Handles.DrawSolidDisc(transform.position, transform.up, 0.25f);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
