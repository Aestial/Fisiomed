using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : Singleton<MainMenuManager>
{
	[SerializeField] private MenuStateVariable state;

	[SerializeField] private float splashScreenTime;
	[SerializeField] private float loadScreenTime;

	// Called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Called second
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
		this.state.RuntimeValue = MenuState.Splash;
        Debug.Log("OnSceneLoaded: " + scene.name);
        // Debug.Log(mode);
    }

	// Called third
	void Start()
	{
		if (this.state.RuntimeValue == MenuState.Splash)
		{
			this.ExitSplash();
		}
	}

	public void GoToState(MenuState state)
	{
		this.state.RuntimeValue = state;
		this.CheckStateForCoroutine(state);
	}
	
	public void GoToState(int state)
	{
		this.GoToState((MenuState)state);
	}

	private void CheckStateForCoroutine(MenuState state)
	{
		if (state == MenuState.Splash)
		{
			this.ExitSplash();
		}
		else if (state == MenuState.Loading)
		{
			this.ExitLoader();
		}
	}

	private void ExitSplash()
	{
		StartCoroutine(this.ExitSplashCoroutine());
	}

	private void ExitLoader()
	{
		StartCoroutine(this.ExitLoadingCoroutine());
	}

	private IEnumerator ExitSplashCoroutine()
	{
		yield return new WaitForSeconds(this.splashScreenTime);
		this.state.RuntimeValue = MenuState.Loading;
		this.ExitLoader();
	}

	private IEnumerator ExitLoadingCoroutine()
	{
		yield return new WaitForSeconds(this.loadScreenTime);
		this.state.RuntimeValue = MenuState.LogMenu;
			
	}
}
