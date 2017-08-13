using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCCameraNavigator : MonoBehaviour {

    public enum Axis
    {
        red,
        green,
        blue
    }

    public Axis strafe;
    public Axis height;
    public Axis forward;
    public Axis rotation;

    public float speed;
    private float rotSpeedMod = 10;

    void Start()
    {
        if (speed <= 0) speed = .1f;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = Vector3.zero;
        Vector3 targetRot = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            switch (forward)
            {
                case (Axis.red):
                    targetPos.x += speed;
                    break;
                case (Axis.green):
                    targetPos.y += speed;
                    break;
                case (Axis.blue):
                    targetPos.z += speed;
                    break;
                default:
                    break;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            switch (forward)
            {
                case (Axis.red):
                    targetPos.x -= speed;
                    break;
                case (Axis.green):
                    targetPos.y -= speed;
                    break;
                case (Axis.blue):
                    targetPos.z -= speed;
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            switch (strafe)
            {
                case (Axis.red):
                    targetPos.x -= speed;
                    break;
                case (Axis.green):
                    targetPos.y -= speed;
                    break;
                case (Axis.blue):
                    targetPos.z -= speed;
                    break;
                default:
                    break;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            switch (strafe)
            {
                case (Axis.red):
                    targetPos.x += speed;
                    break;
                case (Axis.green):
                    targetPos.y += speed;
                    break;
                case (Axis.blue):
                    targetPos.z += speed;
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Plus)) //something is wrong with these?
        {
            switch (height)
            {
                case (Axis.red):
                    targetPos.x += speed;
                    break;
                case (Axis.green):
                    targetPos.y += speed;
                    break;
                case (Axis.blue):
                    targetPos.z += speed;
                    break;
                default:
                    break;
            }
        }
        if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Minus)) // 2
        {
            switch (height)
            {
                case (Axis.red):
                    targetPos.x -= speed;
                    break;
                case (Axis.green):
                    targetPos.y -= speed;
                    break;
                case (Axis.blue):
                    targetPos.z -= speed;
                    break;
                default:
                    break;
            }
        }
        if(Input.GetKey(KeyCode.E))
        {
            switch (rotation)
            {
                case (Axis.red):
                    targetRot.x += speed;
                    break;
                case (Axis.green):
                    targetRot.y += speed;
                    break;
                case (Axis.blue):
                    targetRot.z += speed;
                    break;
                default:
                    break;
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            switch (rotation)
            {
                case (Axis.red):
                    targetRot.x -= speed;
                    break;
                case (Axis.green):
                    targetRot.y -= speed;
                    break;
                case (Axis.blue):
                    targetRot.z -= speed;
                    break;
                default:
                    break;
            }
        }

        this.transform.Translate(targetPos);
        this.transform.Rotate(targetRot * rotSpeedMod);
    }
}
