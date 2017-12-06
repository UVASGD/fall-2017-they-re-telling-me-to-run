using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Pointer_MM : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    public GameObject laserPrefab; // The laser prefab
    private GameObject laser; // A reference to the spawned laser
    private Transform laserTransform; // The transform component of the laser for ease of use
    private Vector3 hitPoint; // Point where the raycast hits

    private GameObject interactingWith;

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
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        interactingWith = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            //Debug.Log("tigger down");
            RaycastHit hit;

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
            {
                Debug.Log("Attempting Raycast");
                Debug.Log("hit: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.GetComponent<FillMMButton>() != null)
                {
                    if (interactingWith != null)
                    {
                        if (interactingWith != hit.collider.gameObject)
                        {
                            interactingWith.GetComponent<FillMMButton>().BeginWaitThenUnfill();
                            interactingWith = hit.collider.gameObject;
                        }
                    }
                    hitPoint = hit.point;
                    ShowLaser(hit);
                    interactingWith.GetComponent<FillMMButton>().BeginFill();
                }
            }
        }
        else if (Controller.GetHairTriggerUp())
        {
            laser.SetActive(false);
            if (interactingWith != null)
            {
                interactingWith.GetComponent<FillMMButton>().BeginWaitThenUnfill();
            }
            interactingWith = null;
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
}
