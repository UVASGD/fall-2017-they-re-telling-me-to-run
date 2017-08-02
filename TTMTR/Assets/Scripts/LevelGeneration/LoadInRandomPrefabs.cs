using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInRandomPrefabs : MonoBehaviour {

	[SerializeField]
	public List<GameObjectAndNumber> objs;

	[System.Serializable]
	public struct GameObjectAndNumber
	{
		[SerializeField]
		public GameObject obj;
		[SerializeField]
		public int num;
	}

	// Use this for initialization
	void Start () {
		for(int i = 0; i < objs.Count; i++){
			for (int j = 0; j < objs[i].num; j++) {
				Instantiate (objs[i].obj, this.gameObject.transform);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}