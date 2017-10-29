using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawn : MonoBehaviour {

    [System.Serializable]
    public struct SignWithCount
    {
        [SerializeField]
        public Sign sign;
        [SerializeField]
        public int count;
    }

    public SignWithCount[] possibleSigns;

	// Use this for initialization
	void Start () {
        BoxCollider boundBox = gameObject.GetComponent<BoxCollider>();
        Vector3 center = boundBox.bounds.center;
        Vector3 size = boundBox.bounds.size;
        float diagonal = size.magnitude;
        int ignoreLayer = ~LayerMask.GetMask("SpawnAreaBoxes");
        foreach (SignWithCount signCount in possibleSigns)
        {
            for (int i=0; i < signCount.count; i++)
            {
                Vector3 newPos = center + new Vector3(
                    (Random.value - 0.5f) * size.x,
                    (Random.value - 0.5f) * size.y,
                    (Random.value - 0.5f) * size.z);
                RaycastHit hit;
                Vector3 direction = new Vector3(Random.value - 0.5f, -Random.value, Random.value - 0.5f);
                if (Physics.Raycast(newPos, direction, out hit, diagonal, ignoreLayer))
                {
                    Instantiate(signCount.sign, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal), gameObject.transform);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
