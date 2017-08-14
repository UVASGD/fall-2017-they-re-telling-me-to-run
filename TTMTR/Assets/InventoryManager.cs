using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public List<GameObject> inventory;

	// Use this for initialization
	void Start () {
        inventory = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddObjectToInventory(GameObject go)
    {
        inventory.Add(go);
    }

    public GameObject GetObjectInInventoryAt(int index)
    {
        GameObject go = inventory[index];
        inventory.Remove(go);
        return go;
    }

    public GameObject GetObjectInInventory(GameObject go)
    {
        if (inventory.Contains(go))
        {
            inventory.Remove(go);
            return go;
        }
        return null;
    }

    public GameObject PeekObjectInIntenvoryAt(int index)
    {
        return inventory[index];
    }

    public GameObject PeekObjectInInventory(GameObject go)
    {
        if (inventory.Contains(go))
        {
            return go;
        }
        return null;
    }
}
