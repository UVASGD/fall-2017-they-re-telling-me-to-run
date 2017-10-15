using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour {

	// Use this for initialization
	public void buttonStartGame() {
		// Change scene when clicked
		SceneManager.LoadScene("Assets/Scenes/Test_Scenes/Laura_Scene.unity", LoadSceneMode.Single);
	}
	
	// Update is called once per frame

}
