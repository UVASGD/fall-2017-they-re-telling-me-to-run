﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{

    public SoundManager soundMgr;
    public GameObject CameraRig;

    private Vector3 forestStart = new Vector3(514.9f, 6.1f, 674.9f);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadCave()
    {
        soundMgr.FadeMMforCave();
        StartCoroutine(LoadYourAsyncScene(1));
    }

    public void LoadForest()
    {
       soundMgr.FadeMMforForest();
       StartCoroutine(LoadYourAsyncScene(2));
       CameraRig.transform.position = forestStart;
    }

    public void LoadYourScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    IEnumerator LoadYourAsyncScene(int index)
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
