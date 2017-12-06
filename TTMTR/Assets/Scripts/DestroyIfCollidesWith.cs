using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfCollidesWith : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "DontCollideWith")
        {
            Destroy(gameObject);
        }
        
    }
}
