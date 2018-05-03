using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUIPanels : MonoBehaviour 
{
	[SerializeField] private RectTransform[] panels;
	[SerializeField] private float disableTime;
	private bool isLast;
	private bool canTrigger;

	// Use this for initialization
	void Start () 
	{
		this.isLast = false;
		this.canTrigger = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((Input.GetMouseButton(0) || Input.touchCount > 0) && this.canTrigger)
		{
			this.TogglePanels();
			StartCoroutine(this.DisableTriggerCoroutine());
			Debug.Log("Screen pressed, wait a little!!");
		}	
	}

	private void TogglePanels() 
	{
		this.isLast = !this.isLast;
		this.panels[0].gameObject.SetActive(!isLast);
		this.panels[1].gameObject.SetActive(isLast);
	}

	private IEnumerator DisableTriggerCoroutine() 
	{
		this.canTrigger = false;
		yield return new WaitForSeconds(this.disableTime);
		this.canTrigger = true;
	}

}
