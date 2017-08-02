using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutSpacingFixer : MonoBehaviour {

    public GridLayoutGroup gridLayoutGroup;
    public RectTransform parentRectTransform;

    public int cols = 0;
    public int rows = 0;

	// Use this for initialization
	void Start () {
        cols = (int)gridLayoutGroup.constraintCount;
        rows = (gridLayoutGroup.gameObject.transform.childCount / cols);

        gridLayoutGroup.cellSize = new Vector2(
            parentRectTransform.rect.width / cols - gridLayoutGroup.spacing.x,
            parentRectTransform.rect.height / rows - gridLayoutGroup.spacing.y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
