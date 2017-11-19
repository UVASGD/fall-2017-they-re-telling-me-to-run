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
    public float delay;
    void Start()
    {
        StartCoroutine(RealStart());
    }

    IEnumerator  RealStart() {
        yield return new WaitForSeconds(delay);
        BoxCollider boundBox = gameObject.GetComponent<BoxCollider>();
        Vector3 center = boundBox.bounds.center;
        Vector3 size = boundBox.bounds.size;
        float diagonal = size.magnitude;
        int ignoreLayer = ~LayerMask.GetMask("SpawnAreaBoxes");
        float coneRadius = 0.25f;
        foreach (SignWithCount signCount in possibleSigns)
        {
            for (int i=0; i < signCount.count; i++)
            {
                Vector3 newPos = center + new Vector3(
                    (Random.value - 0.5f) * size.x,
                    (Random.value - 0.5f) * size.y,
                    (Random.value - 0.5f) * size.z);
                RaycastHit centerHit;
                Vector3 direction = Vector3.zero; // This should be changed
                switch (signCount.sign.location)
                {
                    case Sign.Location.Ceiling:
                        direction = Vector3.up;
                        break;
                    case Sign.Location.Floor:
                        direction = Vector3.down;
                        break;
                    case Sign.Location.Wall:
                        // direction = new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f);
                        direction = Vector3.forward;
                        break;
                }
                if (Physics.Raycast(newPos, direction, out centerHit, diagonal, ignoreLayer))
                {
                    if (signCount.sign.location == Sign.Location.Wall)
                    {
                        Vector3 perp = Vector3.Cross(direction, Vector3.up).normalized*coneRadius; 
                        RaycastHit[] starHits = new RaycastHit[8];
                        Physics.Raycast(newPos, direction, out starHits[0], diagonal, ignoreLayer);
                        // Start at up, continue clockwise (0-U, 1-UR, 2-R, 3-DR, 4-D, 5-DL, 6-L, 7-UL)
                        // Ryan is a genius, just add the vectors

                    }
                    else
                    {
                        Instantiate(signCount.sign, centerHit.point, Quaternion.FromToRotation(Vector3.up, centerHit.normal), gameObject.transform);
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
