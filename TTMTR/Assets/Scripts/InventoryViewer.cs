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

public class InventoryViewer : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    public InventoryManager IM;
    public Canvas view;
    public GameObject collidingObject;

    private float waitBuffer = 0;
    private float waitTime = 0.05f;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    //new
    void Start()
    {

    }

    void Update()
    {
        float xTouch = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[0];
        float yTouch = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0)[1];

        float angle = Mathf.Atan2 (yTouch, xTouch) * Mathf.Rad2Deg - 90;
		if (angle < 0) angle += 360f;
        Debug.Log(angle);
        view.GetComponent<RadialLayoutGroup>().Highlight(angle);
        //Debug.Log(Vector2.Angle(Vector2.zero, new Vector2(xTouch, yTouch)));

        // Is the touchpad held down?
        if (waitBuffer >= waitTime)
        {
            if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (view.gameObject.GetComponent<RadialLayoutGroup>().state == RadialLayoutGroup.ViewState.fanin)
                {
                    view.gameObject.GetComponent<RadialLayoutGroup>().TurnOn();
                }
                else if (view.gameObject.GetComponent<RadialLayoutGroup>().state == RadialLayoutGroup.ViewState.fanout)
                {
                    view.gameObject.GetComponent<RadialLayoutGroup>().TurnOff();
                }
                waitBuffer = 0;
            }
        }
        else
        {
            waitBuffer += Time.unscaledDeltaTime;
        }

        // Touchpad released this frame & valid teleport position found
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {

        }
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
        collidingObject = col.gameObject;
    }


}
