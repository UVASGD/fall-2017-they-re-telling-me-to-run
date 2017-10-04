using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour {

    public List<GameObject> prefabs;
	// Use this for initialization
	void Start () {
        GameObject sign = GameObject.Instantiate(prefabs[Random.Range(0,prefabs.Count)], this.transform);
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.up);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
