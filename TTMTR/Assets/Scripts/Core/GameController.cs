using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	string gameOverScene = "Assets/Scenes/Test_Scenes/Laura_End_Scene.unity";
	string winScene = "Assets/Scenes/Test_Scenes/Talia_Win_Scene.unity";

	public DifficultyController diffCont;

	// Use this for initialization
	void Start () {
		if (diffCont == null) {
			diffCont = GetComponent<DifficultyController> ();
		}
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

	public void IncreaseDifficulty() {
		diffCont.IncDifficulty ();
	}

	public void DecreaseDifficulty() {
		diffCont.DecDifficulty ();
	}

	public void SetDifficulty(int diff) {
		while (diff < diffCont.CurrentDifficulty) {
			diffCont.IncDifficulty ();
		}
		while (diff > diffCont.CurrentDifficulty) {
			diffCont.DecDifficulty ();
		}
	}

	public void RegisterDifficultyObservation(HasDifficulty diff) {
		diffCont.AddDiffObject (diff);
	}
}
