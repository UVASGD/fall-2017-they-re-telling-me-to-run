using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AreaSpawn : MonoBehaviour {

    [System.Serializable]
    public struct SignWithCount
    {
        [SerializeField]
        public Sign sign;
        [SerializeField]
        public int count;
    }

    private SignWithCount[] possibleSigns;

    public SignWithCount[] defaultSigns;

    // Use this for initialization
    public float delay;
    public bool debugWalls;
    void Start()
    {
        StartCoroutine(RealStart());
    }

	[Inject]
	void setSigns([Inject(Id="areaSignsToSpawn")]SignWithCount[] signsToSpawn)
    {
        possibleSigns = signsToSpawn;
    }

    IEnumerator  RealStart() {
        if (possibleSigns == null)
        {
            possibleSigns = defaultSigns;
        }

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
                        Vector3 longways = direction.normalized * centerHit.distance;
                        RaycastHit[] starHits = new RaycastHit[8];
                        // Numbers go clockwise from up (0)
                        Vector3[] starVecs = { Vector3.up * coneRadius, (Vector3.up * coneRadius + perp)/2, perp, (Vector3.down * coneRadius + perp)/2,
                            Vector3.down * coneRadius, (Vector3.up * coneRadius - perp)/2,  -perp, (Vector3.up * coneRadius - perp)/2};
                        for(int j = 0; j < 8; j++)
                        {
                            Physics.Raycast(newPos, longways + starVecs[j], out starHits[j], centerHit.distance*1.25f, ignoreLayer);
                        }
                        if (debugWalls) {
                            for (int j = 0; j < 8; j++)
                            {
                                Instantiate(signCount.sign, starHits[j].point, Quaternion.FromToRotation(Vector3.up, starHits[j].normal), gameObject.transform);
                            }
                        }
                        Vector3 averageNormal = centerHit.normal;
                        int goodVecCount = 1;
                        for(int j = 0; j < 8; j++)
                        {
                            if (centerHit.distance*0.875f <= starHits[j].distance && starHits[j].distance <= centerHit.distance * 1.125f)
                            {
                                averageNormal += starHits[j].normal;
                                goodVecCount++;
                            }
                        }
                        averageNormal /= goodVecCount;
                        Sign newSign = Instantiate(signCount.sign, centerHit.point, Quaternion.LookRotation(averageNormal), gameObject.transform);
                        newSign.gameObject.transform.Rotate(Vector3.up * -90);
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
