using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTool : MonoBehaviour {

    List<InventoryItem> listOfInternalItems = new List<InventoryItem>();
	static Dictionary<InventoryItem, List<InventoryItem>> recipes = new Dictionary<InventoryItem, List<InventoryItem>>();

	// Use this for initialization
	void Start () {
		Debug.Log("hello I'm a crafting tool!2");
		List<string> items = new List<string> ();
		items.Add ("ingredient1");
		items.Add ("ingredient2");
		items.Add ("ingredient3");
		AddRecipe ("ITEMMM", items);
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

	void AddRecipe(string name, List<string> ingredients) {
		InventoryItem item = new InventoryItem ();
		item.name = name;
		List<InventoryItem> itemIngredients = new List<InventoryItem> ();

		foreach (string i in ingredients) {
			InventoryItem item2 = new InventoryItem ();
			item2.name = i;
			itemIngredients.Add (item2);
		}

		recipes.Add (item, itemIngredients);
		Debug.Log ("Recipe added" + recipes.ToString);
	}

}
