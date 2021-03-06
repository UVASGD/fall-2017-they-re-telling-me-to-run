﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CraftingTool : MonoBehaviour {

	public struct Recipe {
		public string creation;
		public Dictionary<string, int> ingredients;

		public Recipe(string item, Dictionary<string, int> itemIngreds) {
			creation = item;
			ingredients = itemIngreds;
		}
	}

	public Dictionary<string, int> listOfInternalItems = new Dictionary<string, int>();
	static List<Recipe> recipes = new List<Recipe>();
	string prefabFilePath = "Prefabs/Objects/";

	public GameController gameCont;

	void Start () {

		XMLReaderTool myReader = new XMLReaderTool("Cave");
	}

	[Inject]
	void Init([Inject(Id="recipeList")]List<Recipe> recs) {
		recipes = recs;
	}

	void Update () {
        //will spawn all held objects when the spacebar is clicked
        if (Input.GetKeyDown("space")) { // TODO: change this?
            ReleaseHeldObjects();
        }
	}

	void OnTriggerEnter(Collider other) {
        InventoryItem script = other.gameObject.GetComponent(typeof(InventoryItem)) as InventoryItem;
        GameObject go = other.gameObject;
		Debug.Log ("COLLISIONNN");
		if (script != null && script.craftable) {
			if (listOfInternalItems.ContainsKey (script.name)) {
				int amount = 0;
				listOfInternalItems.TryGetValue (script.name, out amount);
				listOfInternalItems[script.name] = amount + 1;
			} else {
				listOfInternalItems.Add(script.name, 1);
			}
			Destroy (go);
			CheckRecipes ();
        }
	}

	// Check to see if our ingredients match any recipe
	void CheckRecipes() {
		foreach (Recipe r in recipes) {
			bool haveItems = true;
			KeyValuePair<string, int> ingredientsToThrowOut = new KeyValuePair<string, int>();
			foreach (KeyValuePair<string, int> ingredient in r.ingredients) {
				int amount = 0;
				listOfInternalItems.TryGetValue (ingredient.Key, out amount);
				if (amount < ingredient.Value) {
					haveItems = false;
				}
			}
			if (haveItems) {
				// Spawn new item
				ThrowOut (r.creation, 0);
				// Delete ingredients from listOfInternalItems
				foreach (KeyValuePair<string, int> ingredient in r.ingredients) {
					listOfInternalItems [ingredient.Key] = listOfInternalItems [ingredient.Key] - ingredient.Value;
				}
				gameCont.IncreaseDifficulty ();
			}

		}
	}
		

	void AddRecipe(string name, Dictionary<string, int> ingredients) {
		Debug.Log("adding recipe: " + name);
			recipes.Add (new Recipe (name, ingredients));
	}

    //will spawn the passed object into the map near the crafting table
    //To do - determine the crafting tables location and spawn near it
    void ThrowOut(string name, int i) {
		Debug.Log (prefabFilePath + " " + name);
		GameObject instance = Instantiate(Resources.Load(prefabFilePath + name) as GameObject);
		Vector3 pos = transform.position;
		pos.x += 1 + i;
		pos.y += 1;
		pos.z += 2;
		instance.transform.position = pos;

    }

    //Will spawn all held objects into the game
    void ReleaseHeldObjects() {
		foreach (string x in listOfInternalItems.Keys) {
			
            int amount = 0;
            listOfInternalItems.TryGetValue(x, out amount);

            for (int i = 0; i < amount; i++) {
                ThrowOut(x, i);
            }

        }

		listOfInternalItems.Clear ();
    }

}
