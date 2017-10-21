using UnityEngine;
using UnityEditor;

public class SignSpawn : MonoBehaviour {

    public Sign prefab;
    private Sign spawnedSign;

	// Use this for initialization
	void Start () {
        
	}

    public void spawnSign()
    {
        spawnedSign = GameObject.Instantiate(prefab, this.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Handles.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up);
        Handles.DrawWireDisc(transform.position, transform.up, 0.25f);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
