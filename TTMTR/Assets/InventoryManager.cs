using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public GameObject sceneCameraView;
    public List<Canvas> inventoryViewers;
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
        UpdateVisuals();
        return go;
    }

    public GameObject GetObjectInInventory(GameObject go)
    {
        if (inventory.Contains(go))
        {
            inventory.Remove(go);
            UpdateVisuals();
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

    public void UpdateVisuals()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject item = inventory[i];
            InventoryItem ii = item.GetComponent<InventoryItem>();
            if (ii != null)
            {
                foreach (Canvas canvas in inventoryViewers)
                {
                    if (canvas.transform.childCount > i)
                    {
                        canvas.transform.GetChild(i).
                            GetChild(0).GetComponent<Image>().sprite = ii.sprite;
                    }
                }
                if(sceneCameraView.transform.childCount > i)
                {
                    sceneCameraView.transform.GetChild(i).
                                               GetChild(0).GetComponent<Image>().sprite = ii.sprite;
                }
            }

        }
    }
}
