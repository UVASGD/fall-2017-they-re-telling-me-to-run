using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class HighlightTest : MonoBehaviour {

	[SerializeField]
	private bool lightOn;
	private Shader innocuous;
	private Shader bumped;

	void OnMouseOver()
	{
		//If your mouse hovers over the GameObject with the script attached, output this message
		if (!lightOn) 
		{
			//var innocuous = Shader.Find( "Custom/Innocuous" );
			Debug.Log ("Mouse is over GameObject.");
			if (this.gameObject.GetComponent<MeshRenderer> ().materials.Length > 1) 
			{
				Highlight ();
			} 
			else 
			{
				Debug.LogWarning ("You need at least two materials on " + this.gameObject.name);
			}
		}
	}

	void OnMouseExit()
	{
		//The mouse is no longer hovering over the GameObject so output this message each frame
		//var bumped = Shader.Find( "Outlined/Silhouetted Bumped Diffuse" );
		lightOn = false;
		Debug.Log("Mouse is no longer on GameObject.");
		if (this.gameObject.GetComponent<MeshRenderer> ().materials.Length > 1) 
		{
			LowLight ();
		} 
		else 
		{
			Debug.LogWarning ("You need at least two materials on " + this.gameObject.name);
		}
	}

	// Use this for initialization
	void Start () {
		innocuous = Shader.Find( "Custom/Innocuous" );
		bumped = Shader.Find( "Outlined/Silhouetted Bumped Diffuse" );
		lightOn = false;
		LowLight ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Highlight()
	{
		if (!lightOn) 
		{
			lightOn = true;
			Material mat = this.gameObject.GetComponent<MeshRenderer> ().materials [1];
			mat.shader = bumped;
		}
	}

	public void LowLight()
	{
		lightOn = false;
		Material mat = this.gameObject.GetComponent<MeshRenderer> ().materials [1];
		mat.shader = innocuous;
	}
}
