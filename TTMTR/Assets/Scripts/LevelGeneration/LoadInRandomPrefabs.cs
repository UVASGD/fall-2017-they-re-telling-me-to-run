using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInRandomPrefabs : MonoBehaviour {

	[SerializeField]
	public List<GameObjectAndNumber> objs;

    public bool passDestroyIfCollidesWith;
    public List<Collider> colliders;

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
        Debug.Log(colliders.Count);

        for (int i = 0; i < objs.Count; i++){
			for (int j = 0; j < objs[i].num; j++) {
				GameObject go = Instantiate (objs[i].obj, this.gameObject.transform);
                if (passDestroyIfCollidesWith)
                {
                    DestroyIfCollidesWith dif = go.AddComponent<DestroyIfCollidesWith>();
                }
			}
		}  
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}