using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour 
{
	[SerializeField] private MenuState state;

	private Button button;
	
	void Start ()
	{
		this.button = this.GetComponent<Button>();
		this.button.onClick.AddListener(ClickEvent);
	}

	private void ClickEvent()
	{
		MainMenuManager.Instance.GoToState(this.state);
	}

	void OnDestroy()
	{
		this.button.onClick.RemoveAllListeners();	
	}
}
