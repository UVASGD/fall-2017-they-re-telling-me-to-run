using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData {

	// data structures to contain: monster's recipes, items or whatever
	public static string levelType;
	public static string monsterName;
	
	public static XMLReaderTool reader;
	
	public static List<CraftingTool.Recipe> recipes;
	public static List<string> signs; // TODO: use actual data structure

	// TODO: have some initialize monster data
	
	void Start () {
		// initialize all necessary data structures	
		
		levelType = "Cave"; // TODO: pick automatically? get from XML reader after reading level file?
		reader = new XMLReaderTool(levelType);
		
	}
}
