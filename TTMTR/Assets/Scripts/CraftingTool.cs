using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTool : MonoBehaviour {

	public struct Recipe {
		public string creation;
		public Dictionary<string, int> ingredients;

		public Recipe(string item, Dictionary<string, int> itemIngreds) {
			creation = item;
			ingredients = itemIngreds;
		}
	}

	public Dictionary<string, int> listOfInternalItems;
	static List<Recipe> recipes;
	string prefabFilePath;

	public GameController gameCont;

	void Start () {
		// Test Recipes (TODO: get rid of these two things when DI is finished)
		Debug.Log("I'm initializing a crafting tool1");
		XMLReaderTool myReader = new XMLReaderTool("Cave");
		Debug.Log("I'm initializing a crafting tool1");
		/*
		Dictionary<string, int> testRecipe1 = new Dictionary<string, int> ();
		testRecipe1.Add ("Teleporter", 3);
		AddRecipe ("Teleporter", testRecipe1);
		*/
		Dictionary<string, int> testRecipe2 = new Dictionary<string, int> ();
		testRecipe2.Add ("Spoon", 2);
		testRecipe2.Add ("Cup", 1);
		AddRecipe ("Teleporter", testRecipe2);
	}

	void Init(Dictionary<string, int> internalItems, List<Recipe> recs, string filePath) {
		listOfInternalItems = internalItems;
		recipes = recs;
		prefabFilePath = filePath;
	}

	void Update () {
        //will spawn all held objects when the spacebar is clicked
        if (Input.GetKeyDown("space")) { // TODO: change this?
            ReleaseHeldObjects();
        }
	}

	void OnCollisionEnter(Collision other) {
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

		GameObject instance = Instantiate(Resources.Load(prefabFilePath + name, typeof(GameObject))) as GameObject;
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
