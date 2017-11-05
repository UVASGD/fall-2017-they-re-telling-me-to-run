using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class XMLReaderTool : MonoBehaviour {

	void Start() {
		ReadRecipeFromXML ();

	}


	public static CraftingTool.Recipe ReadRecipeFromXML() {

		string xmlnode = "<Recipe>" +
		                 "<Object>fi_vil_forge_broadsword4</Object>" +
		                 "<Ingredients>" +
		                 "<Ingedient>" +
		                 "<Name>Spoon</Name>" +
		                 "<Quantity>2</Quanitity>" +
		                 "</Ingredient>" +
		                 "<Ingredient>" +
		                 "<Name>Cup</Name>" +
		                 "<Quantity>1</ Quanitity>" +
		                 "</Ingredient>" +
		                 "</Ingredients>" +
		                 "</Recipe>";

		XmlReader xReader = XmlReader.Create (new StringReader (xmlnode));

		string recipeGoal = "";
		Dictionary<string, int> ingredients = new Dictionary<string, int> ();

		while (xReader.Read ()) {
			if (xReader.NodeType == XmlNodeType.Element) {
				if (xReader.Name == "Object") {
					Debug.Log ("an object! " + xReader.Value);
				} else if (xReader.Name == "Ingredient") {
					Debug.Log ("an ingredient! " + xReader.Value);
				}
			}
		}
	}
}
