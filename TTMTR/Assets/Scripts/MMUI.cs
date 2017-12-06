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
	public GameObject backBtn;
    public List<GameObject> difficulties;
    public List<GameObject> debugs;

    public SceneMgr sceneManager;

    // Use this for initialization
    void Start() {
        beginBtn.GetComponent<FillMMButton>().OnFilled = (int i) => OpenDifficultyMenu(i);
        creditBtn.GetComponent<FillMMButton>().OnFilled = (int i) => OpenCreditsMenu(i);
		backBtn.GetComponent<FillMMButton> ().OnFilled = (int i) => ResetToMain (i);
        difficulties[0].GetComponent<FillMMButton>().OnFilled = (int i) => LoadNoviceLevel(i);
        difficulties[1].GetComponent<FillMMButton>().OnFilled = (int i) => LoadAdeptLevel(i);
        difficulties[2].GetComponent<FillMMButton>().OnFilled = (int i) => LoadMasterLevel(i);

        if (sceneManager == null)
        {
            sceneManager = GameObject.Find("scenemgr").GetComponent<SceneMgr>();
        }
    }

    // Update is called once per frame
    void Update() {

    }

    private void OpenDifficultyMenu(int i)
    {
        PrimaryMenu.SetActive(false);
        DifficultySelection.SetActive(true);
    }

    private void OpenCreditsMenu(int i)
    {
        PrimaryMenu.SetActive(false);
        Credtits.SetActive(true);
    }

	private void ResetToMain(int i)
	{
		PrimaryMenu.SetActive(true);
		Credtits.SetActive(false);
	}

    private void LoadNoviceLevel(int i)
    {
        sceneManager.LoadCave();
    }

    private void LoadAdeptLevel(int i)
    {
        sceneManager.LoadForest();
    }

    private void LoadMasterLevel(int i)
    {
        sceneManager.LoadOldCave();
    }

}
