using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbeltSlot : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
	public InventoryItem touchingItem;
    public InventoryItem heldItem;

    public ControllerGrabObject leftController;
    public ControllerGrabObject rightController;

//    private SteamVR_Controller.Device Controller
//    {
//        get { return SteamVR_Controller.Input((int)trackedObj.index); }
//    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateController (leftController);
		UpdateController (rightController);
    }

	private void UpdateController(ControllerGrabObject cont) {
		if (cont == null || cont.Controller == null) 
		{
			return;
		}
		if (cont.Controller.GetHairTriggerUp())
		{
			if (!heldItem && touchingItem && 
                (touchingItem.gameObject == cont.objectInHand || touchingItem.gameObject == cont.lastObjectInHand))
			{
				heldItem = touchingItem;
				touchingItem = null;
				AddJoint(heldItem);
			}
		}
		else if (cont.Controller.GetHairTriggerDown())
		{
			if (heldItem && (heldItem.gameObject == cont.objectInHand ||
				             heldItem.gameObject == cont.collidingObject))
			{
				heldItem = null;
				var joint = GetComponent<FixedJoint>();
				if (joint != null)
				{
					joint.connectedBody = null;
					Destroy(GetComponent<FixedJoint>());
				}
			}
		}
	}

    void OnTriggerEnter(Collider other)
    {
		Debug.Log ("TRIGGER " + other.gameObject.name);
		if (touchingItem != null || heldItem != null) {
			return;
		}
        InventoryItem item = other.GetComponent<InventoryItem>();
		if (item != null && (leftController.objectInHand == other.gameObject ||
			rightController.objectInHand == other.gameObject))
        {
			Debug.Log ("1: Trigger Entered with Toolbelt");
            item.GetComponent<Highlightable>().Highlight();
            GetComponent<Highlightable>().Highlight();
            touchingItem = item;
        }
    }

    void OnTriggerExit(Collider other)
    {
        InventoryItem item = other.GetComponent<InventoryItem>();
        if (item != null && item == touchingItem)
        {
			Debug.Log ("2: Trigger Exited with Toolbelt");
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
