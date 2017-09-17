using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTool : MonoBehaviour {

    List<InventoryItem> listOfInternalItems = new List<InventoryItem>();

	// Use this for initialization
	void Start () {
		Debug.Log("hello I'm a crafting tool!2");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("here's our list of internal items:");
        Debug.Log(listOfInternalItems[0]);
        Debug.Log(listOfInternalItems.Count);
	}

	void OnTriggerEnter(Collider other) {
	    Debug.Log("I've triggered something " + other.name);

        InventoryItem script = other.gameObject.GetComponent(typeof(InventoryItem)) as InventoryItem;
        GameObject go = other.gameObject;

        if (script != null) {
            if (script.craftable == true) {

                listOfInternalItems.Add(script);
                go.SetActive(false);
                go.GetComponent<InventoryItem>().useable = false;

            }

        }

	}
}
