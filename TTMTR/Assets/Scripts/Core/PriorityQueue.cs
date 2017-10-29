using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> {

	private List<T> internalList;

	public int Length {
		get { return this.internalList.Count; }
	}

	public PriorityQueue() {
		internalList = new List<T> ();
	}

	public bool Contains(T t) {
		return internalList.Contains (t);
	}

	public T Peek() {
		if (Length > 0) {
			return internalList [0];
		}
		return default(T);
	}

	public void Push(T t) {
		internalList.Add (t);
		internalList.Sort ();
	}

	public T Pop() {
		if (Length > 0) {
			T item = internalList [0];
			internalList.RemoveAt (0);
			return item;
		}
		return default(T);
	}
}
