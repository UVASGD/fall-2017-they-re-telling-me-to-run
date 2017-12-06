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
    public List<GameObject> difficulties;
    public List<GameObject> debugs;

    public float FADE_DEC_PER_S;

    // Use this for initialization
    void Start() {
        beginBtn.GetComponent<FillMMButton>().OnFilled = (int i) => OpenDifficultyMenu(i);
        creditBtn.GetComponent<FillMMButton>().OnFilled = (int i) => OpenCreditsMenu(i);
        difficulties[0].GetComponent<FillMMButton>().OnFilled = (int i) => OpenCreditsMenu(i);
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

    private void LoadNoviceLevel()
    {

    }

    private void LoadAdeptLevel()
    {

    }

    private void LoadMasterLevel()
    {

    }

    private void LoadCave()
    {
        
    }

    private void LoadForest()
    {

    }

    IEnumerator FadeAudioOut(AudioSource toFade)
    {
        if(toFade.volume > 0)
        {
            toFade.volume -= FADE_DEC_PER_S * Time.deltaTime;
            yield return null;
        }
        toFade.Stop();
    }

}
