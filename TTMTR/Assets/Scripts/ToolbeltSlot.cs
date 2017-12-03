using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbeltSlot : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private InventoryItem touchingItem;
    private InventoryItem heldItem;

    public ControllerGrabObjectAndTeleport leftController;
    public ControllerGrabObjectAndTeleport rightController;

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
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (touchingItem && !heldItem)
            {
                heldItem = touchingItem;
                touchingItem = null;
                AddJoint(heldItem);
            }
        }
        else if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (heldItem && (heldItem == leftController.ObjectInHand ||
                             heldItem == rightController.ObjectInHand))
            {
                heldItem = null;
                var joint = GetComponent<FixedJoint>();
                if (joint)
                {
                    joint.connectedBody = null;
                    Destroy(joint);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (touchingItem || heldItem) return;
        InventoryItem item = other.GetComponent<InventoryItem>();
        if (item && (leftController.ObjectInHand == item ||
                     rightController.ObjectInHand == item))
        {
            item.GetComponent<Highlightable>().Highlight();
            GetComponent<Highlightable>().Highlight();
            touchingItem = item;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InventoryItem item = other.GetComponent<InventoryItem>();
        if (item && item == touchingItem)
        {
            item.GetComponent<Highlightable>().LowLight();
            GetComponent<Highlightable>().LowLight();
            touchingItem = null;
        }
    }

    private void AddJoint(InventoryItem item)
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        fx.connectedBody = item.GetComponent<Rigidbody>();
    }
}
