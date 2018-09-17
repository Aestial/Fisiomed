using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageController : MonoBehaviour 
{
	[SerializeField] float deactiveTime = 10.0f;
	[SerializeField] string defaultValue;
	[SerializeField] TMP_Text messageText;

	// Use this for initialization
	void Start () 
	{
		if (defaultValue != "")
			this.Display(defaultValue);
		else
			this.Deactivate();
	}

	public void Deactivate()
	{
		this.gameObject.SetActive(false);	
	}

	public void Display(string message)
	{
		this.gameObject.SetActive(true);
		messageText.text = message;
		StopAllCoroutines();
		StartCoroutine(DeactiveCoroutine(deactiveTime));
	}

	private IEnumerator DeactiveCoroutine(float time)
	{
		yield return new WaitForSeconds(time);
		this.gameObject.SetActive(false);
	}
}
