using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour {

	List<string> combinables = new List<string>{"potato", "taco"};
	public UnityEngine.UI.Text message;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		// if in combinables
		// do something
		InventoryItem other = collision.gameObject.GetComponent<InventoryItem>();
		if (other != null) {
			if (combinables.Contains (other.name)) {
				Destroy (collision.gameObject);
				//this.GetComponent<GameController> ().winGame ();
			} else {
				StartCoroutine(message.GetComponent<Message>().DisplayMessage ("Sorry you can't combine these"));
			} 
		}
	}


}
