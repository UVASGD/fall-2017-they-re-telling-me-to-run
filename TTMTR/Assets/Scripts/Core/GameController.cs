using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	string gameOverScene = "Assets/Scenes/Test_Scenes/Laura_End_Scene.unity";
	string winScene = "Assets/Scenes/Test_Scenes/Talia_Win_Scene.unity";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

	// Function that handles loss conditions
	public void loseGame() {
		Debug.Log ("You lost");
		SceneManager.LoadScene (gameOverScene, LoadSceneMode.Single);
	}

	public void winGame() {
		Debug.Log ("You win");
		SceneManager.LoadScene (winScene, LoadSceneMode.Single);
	}
}
