using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AppManager : Singleton<AppManager> 
{
	public bool iAmFirst;
	[SerializeField] private GameObject loaderPrefab;
	private Canvas loaderCanvas;

	private int m_CurrentScene;
	private string m_CurrentSceneName;

	// void Awake()
    // {
	// 	#region Don't Destroy OnLoad Singleton
	// 	DontDestroyOnLoad(Instance);
	// 	AppManager[] appManagers = FindObjectsOfType(typeof(AppManager)) as AppManager[];
	// 	if(appManagers.Length > 1)
	// 	{
	// 		for(int i = 0; i < appManagers.Length; i++)
	// 		{
	// 			if(!appManagers[i].iAmFirst)
	// 				DestroyImmediate(appManagers[i].gameObject);
	// 		}
	// 	}
	// 	else
	// 		iAmFirst = true;
	// 	#endregion
    // }

    // called first
    void OnEnable()
    {
        // Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
		GameObject loader = Instantiate(loaderPrefab);
		this.loaderCanvas = loader.GetComponent<Canvas>();
		this.loaderCanvas.enabled = false;
    }

    // called when the game is terminated
    void OnDisable()
    {
        // Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

	private IEnumerator LoadAndChangeCoroutine(int scene, float time)
	{
		this.loaderCanvas.enabled = true;
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene(scene);
	}

	private IEnumerator LoadAndChangeCoroutine(string scene, float time)
	{
		this.loaderCanvas.enabled = true;
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene(scene);
	}

	public void ChangeScene (int scene)
	{
		this.m_CurrentScene = scene;
		StartCoroutine(this.LoadAndChangeCoroutine(scene, 1.1f));
	}

	public void ChangeScene (string sceneName)
	{
		this.m_CurrentSceneName = sceneName;
		StartCoroutine(this.LoadAndChangeCoroutine(sceneName, 1.1f));
	}

	public void ShowLoader(bool show)
	{
		this.loaderCanvas.enabled = show;
	}
}
