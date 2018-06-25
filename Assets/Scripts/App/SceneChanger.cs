using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour 
{
	[SerializeField] private string sceneName;

	private Button button;
	
	void Start () 
	{
		this.button = this.GetComponent<Button>();
		this.button.onClick.AddListener(this.ChangeScene);
	}

	private void ChangeScene()
	{
		AppManager.Instance.ChangeScene(this.sceneName);
	}
	
	void OnDestroy () 
	{
		this.button.onClick.RemoveAllListeners();	
	}
}
