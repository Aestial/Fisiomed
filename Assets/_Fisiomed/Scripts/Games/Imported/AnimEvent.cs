using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour 
{
	public GameState gameState;

	public void SetState(int index)
	{
		gameState.CurrentState = index;
	}
}
