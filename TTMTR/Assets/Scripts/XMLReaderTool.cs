using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class XMLReaderTool {

	static string XML_PATH = "Assets/Scripts/XML/";
	static string levelType;
	static string monsterName;
	
	public XMLReaderTool (string level) {
		levelType = level;
		Initialize ();
	}

	private static void Initialize() {
		Debug.Log ("I'm initializing a xml reader tool!");

		// read level file and choose random monster from list of levels
		ReadLevelFile();
		
		// read appropriate monster file and add these objects to MonsterData.cs
		ReadMonsterFile();

	}
	
	private static void ReadLevelFile() {
		XmlReader reader = XmlReader.Create(XML_PATH + "levels.xml");
		// TODO: find a list of monsters, set actual monster

		while (reader.Read ()) {
			if (reader.NodeType == XmlNodeType.Element && reader.Name == "levelType") {
				// read level types or something
			}
		}

		monsterName = "werewolf";
	}
	
	private static void ReadMonsterFile() {
		XmlReader reader = XmlReader.Create (XML_PATH + monsterName + ".xml");

		while (reader.Read ()) {
			if (reader.NodeType == XmlNodeType.Element) {
				if (reader.Name == "Recipes") {
					ReadSigns (reader.ReadSubtree);
				} else if (reader.Name == "Signs") {
					ReadRecipes (reader.ReadSubtree);
				}
			}
		}
	}

	private static void ReadSigns(XmlReader reader) {
		// TODO: process signs xml
		reader.Close ();

	}

	private static void ReadRecipes(XmlReader reader) {
		while (reader.Read ()) {
			if (reader.NodeType == XmlNodeType.Element && reader.Name == "Recipe") {
				CraftingTool.Recipe recipe = ReadRecipeFromXML (reader.ReadSubtree);
				MonsterData.recipes.Add (recipe);
			}
		}
		reader.Close ();
	}

	// example of extracting info from a string of xml
	private static CraftingTool.Recipe ReadRecipeFromXML(XmlReader reader) {
		string recipeGoal = "";
		Dictionary<string, int> ingredients = new Dictionary<string, int> ();

		while (reader.Read ()) { // Read next element
			if (reader.NodeType == XmlNodeType.Element) {
				if (reader.Name == "Object") {
					reader.Read (); // extract inner text from element
					recipeGoal = reader.Value;
				} else if (reader.Name == "Ingredient") {
					reader.Read ();
					int val;
					ingredients.TryGetValue (reader.Value, out val);
					if (val == 0) {
						ingredients.Add (reader.Value, 1);
					} else {
						ingredients [reader.Value] = val + 1;
					}
				}
			}
		}
		reader.Close ();

		return new CraftingTool.Recipe(recipeGoal, ingredients);
	}
}
