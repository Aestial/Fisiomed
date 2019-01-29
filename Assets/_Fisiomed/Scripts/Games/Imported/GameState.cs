using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour 
{
	[SerializeField] GameObject[] stateContainers;
	[SerializeField] int currentState = 0;

	public int CurrentState 
	{
		get { return currentState; }
		set { SetState(value); }
	}
	
	public void ResetLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void SetState(int index)
	{
		Debug.Log("Turning into State: " + currentState);
		ActiveAll(false);
		ActiveContainer(index, true);
		currentState = index;
	}

	private void ActiveContainer(int index, bool active)
	{
		stateContainers[index].SetActive(active);
	}

	private void ActiveAll(bool active)
	{
		for (int i = 0; i < stateContainers.Length; i++)
		{
			ActiveContainer(i, active);
		}
	}
}
