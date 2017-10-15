using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	// add param for length
	public IEnumerator DisplayMessage(string message, float seconds) {
		GetComponent<UnityEngine.UI.Text> ().text = message;
		// wait like 5 seconds
		yield return new WaitForSeconds(seconds);
		// call clear message
		ClearMessage();

	}

	private void ClearMessage() {
		GetComponent<UnityEngine.UI.Text> ().text = "";
	}
}
