using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCorrectMonster : MonoBehaviour {

	static string MONSTER_PREFABS = "Prefabs/Monsters/";

	// Use this for initialization
	void Start () {
		Debug.Log("Monster Name: " + XMLReaderTool.monsterName);
		GameObject monster = Resources.Load(MONSTER_PREFABS + XMLReaderTool.monsterName, typeof(GameObject)) as GameObject;
		Instantiate(monster, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
