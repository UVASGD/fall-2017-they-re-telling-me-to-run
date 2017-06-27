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

	// Use this for initialization
	void Start () {
		this.gameObject.transform.localScale = new Vector3 (
			Random.Range (minimumScale.x, maximumScale.x), 
			Random.Range (minimumScale.y, maximumScale.y), 
			Random.Range (minimumScale.z, maximumScale.z));
		this.gameObject.transform.localRotation = Quaternion.Euler (
			Random.Range (minimumRot.x, maximumRot.x), 
			Random.Range (minimumRot.y, maximumRot.y), 
			Random.Range (minimumRot.z, maximumRot.z)); 
		this.gameObject.transform.localPosition = new Vector3 (
			Random.Range (minimumPos.x, maximumPos.x), 
			Random.Range (minimumPos.y, maximumPos.y), 
			Random.Range (minimumPos.z, maximumPos.z));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
