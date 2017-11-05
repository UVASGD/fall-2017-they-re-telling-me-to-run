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
        print(Time.time);
        yield return new WaitForSeconds(delay);
        print(Time.time);
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
                        direction = new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f);
                        break;
                }
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
