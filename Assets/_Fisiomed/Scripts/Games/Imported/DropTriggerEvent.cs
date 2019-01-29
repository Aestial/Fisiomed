using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropTriggerEvent : MonoBehaviour 
{
	[SerializeField] DragControl dragObject;
	[SerializeField] string objectTag; 
	[SerializeField] UnityEvent events;
	[SerializeField] bool disableThis = false;
	[SerializeField] bool disableOther = true;

	Collider collider;

	// Use this for initialization
	void Start () 
	{
		collider = GetComponent<Collider>();
	}

	void OnTriggerStay(Collider other)
	{
		Debug.Log("Drop Target Triggered");
		if (dragObject)
		{
			if (!dragObject.isDragged)
			if(dragObject.gameObject == other.gameObject)
			{
				dragObject.transform.position = transform.position;
				events.Invoke();
				if (disableThis)
					collider.enabled = false;
				if (disableOther)
					other.enabled = false;
			}
		}
		else if(!other.GetComponent<DragControl>().isDragged && other.tag == objectTag)
		{
			other.transform.position = transform.position;
			events.Invoke();
			if (disableThis)
				collider.enabled = false;
			if (disableOther)
				other.enabled = false;
		}
		
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
