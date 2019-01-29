using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DragControl : MonoBehaviour 
{
	void Awake()
	{
		this.GetComponent<Collider>().isTrigger = true;
		this.GetComponent<Rigidbody>().isKinematic = true;
	}
	// public bool canBeDragged;
	public bool isDragged;

	private void OnMouseDown()
	{
		isDragged = true;
	}

	private void OnMouseUp()
	{
		isDragged = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isDragged)
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = Camera.main.nearClipPlane;
			Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
			mouseWorld.z = 0;
			transform.position = mouseWorld;
		}	
	}
}
