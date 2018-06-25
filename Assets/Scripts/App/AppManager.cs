using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AppManager : Singleton<AppManager> 
{
	public bool iAmFirst;

	private int m_CurrentScene;

	void Awake()
    {
		#region Don't Destroy OnLoad Singleton
		DontDestroyOnLoad(Instance);
		AppManager[] appManagers = FindObjectsOfType(typeof(AppManager)) as AppManager[];
		if(appManagers.Length > 1)
		{
			for(int i = 0; i < appManagers.Length; i++)
			{
				if(!appManagers[i].iAmFirst)
					DestroyImmediate(appManagers[i].gameObject);
			}
		}
		else
			iAmFirst = true;
		#endregion
    }

	public void ChangeScene (int scene)
	{
		this.m_CurrentScene = scene;
		SceneManager.LoadScene(scene);
	}

	public void ChangeScene (string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
