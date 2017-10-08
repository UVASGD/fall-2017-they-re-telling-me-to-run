using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTool : MonoBehaviour {

	public struct Recipe {
		public InventoryItem creation;
		public List<InventoryItem> ingredients;

		public Recipe(InventoryItem item, List<InventoryItem> itemIngreds) {
			creation = item;
			ingredients = itemIngreds;
		}
	}

    List<InventoryItem> listOfInternalItems = new List<InventoryItem>();
	static List<Recipe> recipes = new List<Recipe>();

	// Use this for initialization
	void Start () {
		Debug.Log("hello I'm a crafting tool!2");
		List<string> items = new List<string> ();
		items.Add ("Capsule");
		items.Add ("Capsule");
		items.Add ("Capsule");
		AddRecipe ("Sphere", items);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("here's our list of internal items:");
        //Debug.Log(listOfInternalItems[0]);
        //Debug.Log(listOfInternalItems.Count);
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

	// Check to see if our ingredients match any recipe
	void CheckRecipes() {
		 

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

		recipes.Add (new Recipe(item, itemIngredients));

		Debug.Log ("~recipes thus far~");
		foreach (Recipe r in recipes) {
			Debug.Log (r.creation.name);

			foreach (InventoryItem i in r.ingredients) {
				Debug.Log (i.name);
			}
		}

	}


}
