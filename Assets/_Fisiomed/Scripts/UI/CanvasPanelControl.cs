using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPanelControl : MonoBehaviour 
{
	[SerializeField] private bool enabledOnAwake = false;
	
	void Awake () 
	{
		this.GetComponent<Canvas>().enabled = enabledOnAwake;
	}
}
