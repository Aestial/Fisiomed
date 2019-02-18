using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GenericButton : MonoBehaviour 
{
    
    [Tooltip("Seconds after each click to wait for a follow-up")]
    public float timeLimit = 0.25f;

    [Tooltip("Which mouse/stylus button to react to")]
    public PointerEventData.InputButton button;

    // Expose events we can wire-up in the inspector to our desired handlers.

    // I added this so we can use it to show visible feedback immediately
    // on the first click, to minimize perceived latency.
    [System.Serializable]
    public class OnAtLeastOneClick : UnityEvent { };
    public OnAtLeastOneClick onAtLeastOneClick;

    [System.Serializable]
    public class OnSingleClick : UnityEvent { };
    public OnSingleClick onSingleClick;

    [System.Serializable]
    public class OnDoubleClick : UnityEvent { };
    public OnDoubleClick onDoubleClick;

    [System.Serializable]
    public class OnTripleClick : UnityEvent { };
    public OnTripleClick onTripleClick;

	[Tooltip("Multiple times value")]
    public int multipleTimes = 5;

	[System.Serializable]
    public class OnMultipleClick : UnityEvent { };
    public OnTripleClick onMultipleClick;

    // Internal state for keeping track of clicks.
    private int clickCount;
    private Coroutine delayedClick;

	void OnMouseDown()
    {
        // Count up the clicks.
        clickCount++;

        // React accordingly.
        switch(clickCount)
        {
            // First click: fire OnAtLeastOneClick and wait to see if a second comes in.
            case 1:
                delayedClick = StartCoroutine(DelayClick(onSingleClick, timeLimit));
				Debug.Log("<color=cyan>ClickController</color> - Single!");
                onAtLeastOneClick.Invoke();
                break;
            // Second click: cancel single-click and wait to see if a third comes in.
            case 2:
                StopCoroutine(delayedClick);
                delayedClick = StartCoroutine(DelayClick(onDoubleClick, timeLimit));
				Debug.Log("<color=cyan>ClickController</color> - Double!");
                break;
            // Third click: cancel double-click fire OnTripleClick immediately.
            case 3:
                StopCoroutine(delayedClick);
                delayedClick = StartCoroutine(DelayClick(onTripleClick, timeLimit));
                // delayedClick = null;
                // onTripleClick.Invoke();
                // clickCount = 0;
				Debug.Log("<color=cyan>ClickController</color> - Triple!");
                break;
        }

		if (clickCount == multipleTimes)
		{
			StopCoroutine(delayedClick);
			delayedClick = null;
			onMultipleClick.Invoke();
            clickCount = 0;
			Debug.Log("<color=cyan>ClickController</color> - Multiple!");
		}

        Debug.Log("<color=cyan>GenericButton</color> - Clicked!");
    }
    
    private IEnumerator DelayClick(UnityEvent clickEvent, float delay)
    {
        yield return new WaitForSeconds(delay);
        // This coroutine didn't get stopped, so no new click came in.
        // Fire off the corresponding click event and reset.
        clickEvent.Invoke();
        clickCount = 0;
        delayedClick = null;
    }
}