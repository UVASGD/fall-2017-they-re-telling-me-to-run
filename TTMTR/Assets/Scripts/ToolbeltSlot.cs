using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbeltSlot : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private InventoryItem touchingItem;
    private InventoryItem heldItem;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)
            && touchingItem && !heldItem)
        {
            heldItem = touchingItem;
            touchingItem = null;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        InventoryItem item = other.GetComponent<InventoryItem>();
        if (item)
        {
            item.GetComponent<Highlightable>().Highlight();
            GetComponent<Highlightable>().Highlight();
            touchingItem = item;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InventoryItem item = other.GetComponent<InventoryItem>();
        if (item)
        {
            item.GetComponent<Highlightable>().LowLight();
            GetComponent<Highlightable>().LowLight();
            touchingItem = null;
        }
    }
}
