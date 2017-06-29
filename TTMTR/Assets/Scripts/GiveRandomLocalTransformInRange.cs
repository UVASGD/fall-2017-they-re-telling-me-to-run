using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveRandomLocalTransformInRange : MonoBehaviour {

	public Vector3 minimumPos;
	public Vector3 maximumPos;
	public Vector3 minimumRot;
	public Vector3 maximumRot;
	public Vector3 minimumScale;
	public Vector3 maximumScale;

	public string mustCollideWith;
	public int maxNumberOfRepositions;
	private int repositionAttempts;

	// Use this for initialization
	void Start () {
		repositionAttempts = 0;
		Rescale ();
		Rerotate ();
		Reposition ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Rescale(){
		this.gameObject.transform.localScale = new Vector3 (
			Random.Range (minimumScale.x, maximumScale.x), 
			Random.Range (minimumScale.y, maximumScale.y), 
			Random.Range (minimumScale.z, maximumScale.z));
	}

	public void Rerotate(){
		this.gameObject.transform.localRotation = Quaternion.Euler (
			Random.Range (minimumRot.x, maximumRot.x), 
			Random.Range (minimumRot.y, maximumRot.y), 
			Random.Range (minimumRot.z, maximumRot.z)); 
	}

	public void Reposition(){
		this.gameObject.transform.localPosition = new Vector3 (
			Random.Range (minimumPos.x, maximumPos.x), 
			Random.Range (minimumPos.y, maximumPos.y), 
			Random.Range (minimumPos.z, maximumPos.z));
		RaycastHit outinfo;
		if (Physics.Raycast (transform.position, Vector3.down, out outinfo)) {
			if (outinfo.collider.CompareTag (mustCollideWith)) {
				return;
			}
		}
		if (repositionAttempts < maxNumberOfRepositions) {
			repositionAttempts++;
			Reposition ();
		} else {
			Destroy (this.gameObject);
		}
	}
}
