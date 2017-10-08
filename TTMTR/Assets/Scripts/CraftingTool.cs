using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTool : MonoBehaviour {

	public struct Recipe {
		public string creation;
		public Dictionary<string, int> ingredients = new Dictionary<string, int>();

		public Recipe(string item, Dictionary<string, int> itemIngreds) {
			creation = item;
			ingredients = itemIngreds;
		}
	}

	public Dictionary<string, int> listOfInternalItems = new Dictionary<string, int>();
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

        //will spawn all held objects when the spacebar is clicked
        //is a temporary solution
        if (Input.GetKeyDown("space")) {
            ReleaseHeldObjects();
        }
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
				CheckRecipes ();
            }

        }

	}

	// Check to see if our ingredients match any recipe
	void CheckRecipes() {
		Dictionary<string, int> 

	}

	void AddRecipe(string name, Dictionary<string, int> ingredients) {


		recipes.Add (new Recipe(name, ingredients));

		Debug.Log ("~recipes thus far~");
		foreach (Recipe r in recipes) {
			Debug.Log (r.creation);

			foreach (KeyValuePair<string, int> i in r.ingredients) {
				Debug.Log (i.Key + "amount: " + i.Value);
			}
		}

	}

    //will spawn the passed object into the map near the crafting table
    //To do - determine the crafting tables location and spawn near it
    //      - instantiate a specific object not just a teleporter
    //      - once this is proerly implemented fix up method ReleaseHeldObjects
    void ThrowOut()
    {

        GameObject instance = Instantiate(Resources.Load("Prefabs/Objects/Teleporter", typeof(GameObject))) as GameObject;
        instance.transform.position = new Vector3(x: 5, y: 5, z: 5);

    }

    //Will spawn all held objects into the game
    //tempporarily weird until method ThrowOut is implemented
    void ReleaseHeldObjects()
    {

        ThrowOut();

        /*
        for(int i = listOfInternalItems.Count; i >= 0; i--) {

            ThrowOut(listOfInternalItems[i]);
            listOfInternalItems.Remove(listOfInternalItems[i]);

        }
        */
    }

}
