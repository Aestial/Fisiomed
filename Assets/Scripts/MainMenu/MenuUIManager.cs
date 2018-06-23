using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct View
{
	public MenuState state;
	public Canvas canvas;
}

public class MenuUIManager : MonoBehaviour 
{
	[SerializeField] private MenuStateVariable startState;
	[SerializeField] private View[] views;

	void Start()
	{
		this.ActiveAll(true);
		this.ChangeCanvas(this.startState.RuntimeValue);
	}

	void OnEnable ()
	{
		this.startState.OnStateChanged += this.ChangeCanvas;
	}

	void OnDisable ()
	{
		this.startState.OnStateChanged -= this.ChangeCanvas;
	}

	private void ChangeCanvas (MenuState state)
	{
		for (int i = 0; i < this.views.Length; i++)
		{
			View view = this.views[i];
			if (view.canvas == null)
				continue;
			if (view.state == state)
				view.canvas.enabled = true;
			else
				view.canvas.enabled = false;
		}
	}

	private void ActiveAll(bool active)
	{
		for (int i = 0; i < this.views.Length; i++)
		{
			View view = this.views[i];
			view.canvas.gameObject.SetActive(active);
		}
	}
}
