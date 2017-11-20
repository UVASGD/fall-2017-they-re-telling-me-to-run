using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class XMLReaderTool {

	static string XML_PATH = "Assets/Scripts/XML/";
	static string levelType;
	static string monsterName;
    static List<string> spotSignsPrefabs;   //this and areaSigns are temp, and will likely be changed inorder to integrate with signs
    static Dictionary<int, string> areaSigns;
	
	public XMLReaderTool (string level) {
		Initialize ();
	}

	public static void Initialize() {
		Debug.Log ("I'm initializing a xml reader tool!");

		// read level file and choose random monster from list of levels
		ReadLevelFile();
		
		// read appropriate monster file and add these objects to MonsterData.cs
		ReadMonsterFile();

	}
	
	private static void ReadLevelFile() {
		XmlReader reader = XmlReader.Create(XML_PATH + "levels.xml");
		// TODO: find a list of monsters, set actual monster

		List<string> levelTypes = new List<string> ();

		while (reader.Read ()) {
			if (reader.NodeType == XmlNodeType.Element && reader.Name == "levelType") {
				levelTypes.Add (reader.GetAttribute("name"));
			}
		}

		System.Random rnd = new System.Random ();

		int levelSelection = rnd.Next (0, levelTypes.Count);
		levelType = levelTypes [levelSelection];

		XmlReader readerMonster = XmlReader.Create(XML_PATH + "levels.xml");

		while (readerMonster.Read ()) {
			if (readerMonster.NodeType == XmlNodeType.Element && readerMonster.GetAttribute("name") == levelType) {
				SelectMonster (readerMonster.ReadSubtree ());
				break;
			}
		}

	}

	private static void SelectMonster(XmlReader reader) {

		List<string> monsters = new List<string> ();

		System.Random rnd = new System.Random ();

		while (reader.Read ()) {
			if (reader.NodeType == XmlNodeType.Element && reader.Name == "monster") {
				monsters.Add (reader.GetAttribute("name"));
			}
		}

		int monsterSelection = rnd.Next (0, monsters.Count);
		monsterName = monsters [monsterSelection];
	}
	
	private static void ReadMonsterFile() {
		XmlReader reader = XmlReader.Create (XML_PATH + monsterName + ".xml");

		while (reader.Read ()) {
			if (reader.NodeType == XmlNodeType.Element) {
				if (reader.Name == "Signs") {
					if (reader.Name == "AreaSpawns") {
                        ReadAreaSigns(reader.ReadSubtree());
                    } else if (reader.Name == "SpotSigns") {
                        ReadSpotSigns(reader.ReadSubtree());
                    }
				} else if (reader.Name == "Recipes") {
					ReadRecipes (reader.ReadSubtree ());
				} else if (reader.Name == "WinConditions") {
					ReadWinConditions (reader.ReadSubtree ());
				} else if (reader.Name == "Paths") {
					ReadPaths (reader.ReadSubtree ());
				}
			}
		}
	}

    /*
     * This method will be called from ReadMonsterFile() and will read in an <AreaSigns> section of the xml file, 
     * which is composed of <signs>s holding <prefab>s and <count>s
     */

    private static void ReadAreaSigns (System.Xml.XmlReader reader) {
        int count = -10;    //integers can't be set to null, so -10 is an impossible value that's a stand in for null
        string prefab = null;

        while (reader.Read()) {
            if (reader.NodeType == XmlNodeType.Element) {

                if (reader.Name == "prefab") {
                    prefab = reader.Value;
                } else if (reader.Name == "count") {
                    int.TryParse(reader.Value, out count);
                }

                //if a value for both prefab and count has been found...
                if (count != -10 && prefab != null) {
                    areaSigns.Add(count, prefab);

                    count = -10;
                    prefab = null;
                }

            }
        }

        reader.Close();
    }

    /*
     * This method will be called from ReadMonsterFile() and will read in an <SpotSigns> section of the xml file, 
     * which is composed of <signs>s holding only <prefab>s
     */
    private static void ReadSpotSigns(System.Xml.XmlReader reader) {
        while (reader.Read()) {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "prefab") {
                spotSignsPrefabs.Add(reader.Value);
            }
        }

        reader.Close();
    }


    /*
     * Ending read signs sub-section
     * Begining read recipes sub-section
     */

    private static void ReadRecipes(XmlReader reader) {
		while (reader.Read ()) {
			if (reader.NodeType == XmlNodeType.Element && reader.Name == "Recipe") {
				CraftingTool.Recipe recipe = ReadRecipeFromXML (reader.ReadSubtree());
				MonsterData.recipes.Add (recipe);
			}
		}
		reader.Close ();
	}

	private static void ReadWinConditions(XmlReader reader) {
		while (reader.Read ()) {

		}
	}

	private static void ReadPaths (XmlReader reader) {
		while (reader.Read ()) {

		}
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