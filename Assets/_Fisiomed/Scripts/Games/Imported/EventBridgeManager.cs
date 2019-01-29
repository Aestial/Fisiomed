using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventBridge 
{
	public string name;
	public UnityEvent outputs;
}

public class EventBridgeManager : MonoBehaviour 
{
	public GameState gameState;
	public EventBridge[] bridges;
	
	public void TriggerEvent(int index)
	{
		bridges[index].outputs.Invoke();
	}

	public void TriggerEvent(string name)
	{
		for (int i = 0; i < bridges.Length; i++)
		{
			if(bridges[i].name == name && i == gameState.CurrentState)
			{
				bridges[i].outputs.Invoke();
				break;
			}
		}
	}
}
