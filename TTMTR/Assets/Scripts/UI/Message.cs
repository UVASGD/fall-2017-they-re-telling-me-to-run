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

	public IEnumerator DisplayMessage(string message) {
		GetComponent<UnityEngine.UI.Text> ().text = message;
		// wait like 5 seconds
		yield return new WaitForSeconds(3);
		// call clear message
		ClearMessage();

	}

	private void ClearMessage() {
		GetComponent<UnityEngine.UI.Text> ().text = "";
	}
}
