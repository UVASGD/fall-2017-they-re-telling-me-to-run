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

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "DontCollideWith")
        {
            Debug.Log("wreak this mofo");
            Destroy(gameObject);
        }
        
    }
}
