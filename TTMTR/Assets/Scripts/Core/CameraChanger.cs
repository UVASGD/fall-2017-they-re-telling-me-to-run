using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour {

    public Camera pc_alternativeCamera;

    // Use this for initialization
    void Start()
    {
        if (SteamVR.instance == null)
        {
            if (pc_alternativeCamera != null)
            {
                pc_alternativeCamera.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
		UnityEngine.XR.XRSettings.showDeviceView = Globals.MATCH_DISPLAYS_FOR_HMD_AND_MONITOR; // run this line regardless of if in unity editor or not
    }
	
}
