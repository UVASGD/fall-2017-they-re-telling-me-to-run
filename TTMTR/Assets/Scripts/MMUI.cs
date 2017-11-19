using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMUI : MonoBehaviour {

	public GameObject PrimaryMenu;
	public GameObject Credtits;
	public GameObject DifficultySelection;
	public GameObject DebugMenu;

	public GameObject beginBtn;
	public GameObject creditBtn;
	public List<GameObject> dificulties;
	public List<GameObject> debugs;

	// Use this for initialization
	void Start () {
		beginBtn.GetComponent<FillMMButton> ().OnFilled = (int i) => OpenDifficultyMenu (i);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OpenDifficultyMenu(int i)
	{
		PrimaryMenu.SetActive (false);
		DifficultySelection.SetActive (true);
	}
}
