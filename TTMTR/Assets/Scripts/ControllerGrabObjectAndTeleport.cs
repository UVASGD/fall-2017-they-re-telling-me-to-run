/*
 * Copyright (c) 2016 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

public class ControllerGrabObjectAndTeleport : MonoBehaviour
{

    bool triggerIsDown;

    public InventoryManager IM;
    public InventoryViewer otherInventoryView;

    //----------------------object in hand
    private SteamVR_TrackedObject trackedObj;

    public GameObject collidingObject;
    [SerializeField]
    public GameObject objectInHand;
    // -------------------------------------

    //teleporting 
    public Transform cameraRigTransform;
    public Transform headTransform; // The camera rig's head
    public Vector3 teleportReticleOffset; // Offset from the floor for the reticle to avoid z-fighting
    public LayerMask teleportMask; // Mask to filter out areas where teleports are allowed

    public GameObject laserPrefab; // The laser prefab
    private GameObject laser; // A reference to the spawned laser
    private Transform laserTransform; // The transform component of the laser for ease of use

    public GameObject teleportReticlePrefab; // Stores a reference to the teleport reticle prefab.
    private GameObject reticle; // A reference to an instance of the reticle
    private Transform teleportReticleTransform; // Stores a reference to the teleport reticle transform for ease of use

    private Vector3 hitPoint; // Point where the raycast hits
    private bool shouldTeleport; // True if there's a valid teleport target
    //------------------------------------------

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    void Start()
    {
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }

    public bool IsHandEmptyAndAlone()
    {
        return objectInHand == null && collidingObject == null;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        Debug.Log("Now colliding with " + col.gameObject.name);
        collidingObject = col.gameObject;
    }

    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            triggerIsDown = true;
            if (objectInHand && objectInHand.tag == "Teleporter")
            {
                if (objectInHand.GetComponent<InventoryItem>().useable)
                {
                    SearchForTeleport();
                }
            }
            else
            {
                if (collidingObject)
                {
                    Debug.Log("I should grab " + collidingObject.name);
                    GrabObject();
                    GetComponent<InventoryViewer>().view.GetComponent<RadialLayoutGroup>().TurnOff();
                }
            }
        }
        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }

        if (triggerIsDown)
        {
            if (objectInHand && objectInHand.tag == "Teleporter")
            {
                if (objectInHand.GetComponent<InventoryItem>().useable)
                {
                    SearchForTeleport();
                }
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            triggerIsDown = false;
            if (objectInHand)
            {
                if (objectInHand == otherInventoryView.collidingObject)
                {
                    ReleaseObject();
                    IM.AddObjectToInventory(otherInventoryView.collidingObject);
                    otherInventoryView.collidingObject = null;
                }
                else
                {
                    if (objectInHand.tag == "Teleporter")
                    {
                        if (objectInHand.GetComponent<InventoryItem>().useable)
                        {
                            Teleport();
                        }
                    }
                    else
                    {
                        ReleaseObject();
                    }
                }

                InventoryItem ii = objectInHand.GetComponent<InventoryItem>();
                if (ii != null)
                {
                    ii.useable = true;
                }
            }
        }
    }

    private void SearchForTeleport()
    {
        RaycastHit hit;

        // Send out a raycast from the controller
        if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
        {
            if (hit.collider.CompareTag("CanTeleportTo"))
            {
                if (Vector3.Distance(Camera.main.transform.position, hit.point + teleportReticleOffset) <= Globals.MAXIMUM_TELEPORT_DISTANCE)
                {

                    hitPoint = hit.point;
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                    shouldTeleport = true;

                }
                ShowLaser(hit);

                //Show teleport reticle
                reticle.SetActive(true);
            }
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true); //Show the laser
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f); // Move laser to the middle between the controller and the position the raycast hit
        laserTransform.LookAt(hitPoint); // Rotate laser facing the hit point
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance); // Scale laser so it fits exactly between the controller & the hit point
    }

    private void Teleport()
    {
        shouldTeleport = false; // Teleport in progress, no need to do it again until the next touchpad release
        reticle.SetActive(false); // Hide reticle
        Vector3 difference = cameraRigTransform.position - headTransform.position; // Calculate the difference between the center of the virtual room & the player's head
        difference.y = 0; // Don't change the final position's y position, it should always be equal to that of the hit point

        cameraRigTransform.position = hitPoint + difference; // Change the camera rig position to where the the teleport reticle was. Also add the difference so the new virtual room position is relative to the player position, allowing the player's new position to be exactly where they pointed. (see illustration)
    }

    public void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
        objectInHand.SetActive(true);
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        InventoryManager IM = GetComponent<InventoryViewer>().IM;
        IM.GetObjectInInventory(objectInHand); 
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    public void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        objectInHand = null;
    }

    public bool IsHandEmpty()
    {
        return (objectInHand == null);
    }

    public bool IsHandFull()
    {
        return (objectInHand != null);
    }
}
