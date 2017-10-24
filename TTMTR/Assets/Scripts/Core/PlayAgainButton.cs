using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour {
	//public Button yourButton;

	//void Start () {
		//Button btn = yourButton.GetComponent<Button> ();
		//yourButton.onClick.AddListener (TaskOnClick);
	//}

	public void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
		SceneManager.LoadScene ("Assets/Scenes/Caves/Cave1VR.unity", LoadSceneMode.Single);
	}
}
