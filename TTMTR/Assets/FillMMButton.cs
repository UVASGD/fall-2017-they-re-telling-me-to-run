using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class FillMMButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField]
	private Image colorButton;
	private float AMOUNT_PER_MS = 0.1f;

	private float TIME_TO_WAIT = 1.2f;
	[SerializeField]
	private float waited;

	public Action<int> OnFilled;

	// Use this for initialization
	void Start () {
		colorButton = this.gameObject.transform.GetChild(0).GetComponent<Image> ();
		waited = 0.0f;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		BeginFill ();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		BeginWaitThenUnfill ();
	}

    void OnTriggerEnter(Collider coll)
    {
        BeginFill();
    }

    void OnTriggerExit(Collider coll)
    {
        BeginWaitThenUnfill();
    }

    public void BeginFill()
	{
		StopAllCoroutines ();
		StartCoroutine (Fill());
	}

	public void BeginWaitThenUnfill()
	{
		StopAllCoroutines ();
		waited = 0.0f;
		StartCoroutine (WaitThenUnfill());
	}

	IEnumerator Fill()
	{
		while (colorButton.fillAmount < 1) 
		{
			colorButton.fillAmount += Time.deltaTime * AMOUNT_PER_MS;
			yield return null;
		}
		if (OnFilled != null) OnFilled (-1);
	}

	IEnumerator Unfill ()
	{
		while (colorButton.fillAmount > 0) 
		{
			colorButton.fillAmount -= Time.deltaTime * AMOUNT_PER_MS;
			yield return null;
		}
	}

	IEnumerator WaitThenUnfill ()
	{
		while (waited < TIME_TO_WAIT) 
		{
			waited += Time.deltaTime;
			yield return null;
		}
		StartCoroutine (Unfill());
	}
}
