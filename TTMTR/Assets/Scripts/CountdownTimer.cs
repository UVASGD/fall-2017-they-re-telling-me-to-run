using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour {
	public int timekeep = 10;
	public float timer = 10;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		timer -= (Time.deltaTime);
		if (timer < timekeep - 1) {
			timekeep = timekeep - 1;
			GetComponent<Text>().text = timekeep.ToString();
		}
		if (timekeep == 0) {
			SceneManager.LoadScene("Assets/Scenes/Caves/Cave1VR.unity", LoadSceneMode.Single);
		}
	}
}
