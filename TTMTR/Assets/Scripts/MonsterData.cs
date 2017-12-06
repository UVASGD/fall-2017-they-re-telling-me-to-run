using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData {

	// data structures to contain: monster's recipes, items or whatever
	public static string levelType;
	public static string monsterName;
	
	public static List<CraftingTool.Recipe> recipes = new List<CraftingTool.Recipe>();

	public static void Initialize() {
		XMLReaderTool.Initialize();
	}
		
}
