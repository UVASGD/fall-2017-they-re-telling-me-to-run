using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTransformForTime : MonoBehaviour {

    public float timeToWait;
    public float timer;

    private Vector3 position;
    private Quaternion rotation;
    private Vector3 scale;

    // Use this for initialization
    void Start()
    {
        position = this.transform.localPosition;
        rotation = this.transform.localRotation;
        scale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= timeToWait)
        {
            this.transform.localPosition = position;
            this.transform.localRotation = rotation;
            this.transform.localScale = scale;
            Rigidbody rb = this.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.drag = 0;
                rb.angularDrag = 0;
            }
            timer += Time.deltaTime;

        }
    }
}
