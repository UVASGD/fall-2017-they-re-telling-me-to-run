using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.VR.VRSettings.showDeviceView = Globals.MATCH_DISPLAYS_FOR_HMD_AND_MONITOR;
	}
	
}
