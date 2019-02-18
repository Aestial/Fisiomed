using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeEventController : MonoBehaviour 
{
	[System.Serializable]
    public class OnSwipeUp : UnityEvent { };
    public OnSwipeUp onSwipeUp;

    [System.Serializable]
    public class OnSwipeRight : UnityEvent { };
    public OnSwipeRight onSwipeRight;

    [System.Serializable]
    public class OnSwipeDown : UnityEvent { };
    public OnSwipeDown onSwipeDown;

    [System.Serializable]
    public class OnSwipeLeft : UnityEvent { };
    public OnSwipeLeft onSwipeLeft;
	
	void Update () 
	{
		if (SwipeManager.IsSwipingDown()) {
			onSwipeDown.Invoke();
			Debug.Log("<color=orange>GameInput</color> - Swiped Down");
		}
		if (SwipeManager.IsSwipingLeft()) {
			onSwipeLeft.Invoke();
			Debug.Log("<color=orange>GameInput</color> - Swiped Left");
		}
		if (SwipeManager.IsSwipingUp()) {
			onSwipeUp.Invoke();
			Debug.Log("<color=orange>GameInput</color> - Swiped Up");
		}
		if (SwipeManager.IsSwipingRight()) {
			onSwipeRight.Invoke();
			Debug.Log("<color=orange>GameInput</color> - Swiped Right");
		}
	}
}
