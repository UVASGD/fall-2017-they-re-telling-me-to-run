using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawn : MonoBehaviour {

    public Sign[] possibleSigns;

	// Use this for initialization
	void Start () {
        BoxCollider boundBox = gameObject.GetComponent<BoxCollider>();
        Vector3 center = boundBox.bounds.center;
        Vector3 size = boundBox.bounds.size;
        float diagonal = size.magnitude;
        int ignoreLayer = ~(1 << 10);
        foreach (Sign sign in possibleSigns)
        {
            Vector3 newPos = center + new Vector3(
                (Random.value - 0.5f) * size.x,
                (Random.value - 0.5f) * size.y,
                (Random.value - 0.5f) * size.z);
            RaycastHit hit;
            Vector3 direction = new Vector3(Random.value-0.5f,-Random.value,Random.value-0.5f);
            if(Physics.Raycast(newPos, direction, out hit, diagonal,ignoreLayer))
            {
                Instantiate(sign, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal),gameObject.transform);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
