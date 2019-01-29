using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorModifyParameter : MonoBehaviour 
{
	[SerializeField] float increment;
	[SerializeField] float decrement;
	[SerializeField] float delay;
	[SerializeField] float onValue;
	Animator animator;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
	}

	public void TurnOnFloat(string name)
	{
		StartCoroutine(TurnOnFloatCoroutine(name));
	}

	public void PunchFloat(string name)
	{
		float currentValue = animator.GetFloat(name);
		float newValue = currentValue + increment;
		if (newValue > onValue) newValue = onValue;
		animator.SetFloat(name, newValue);
		StopAllCoroutines();
		StartCoroutine(ResetFloatCoroutine(name, delay));
	}

	IEnumerator TurnOnFloatCoroutine(string name)
	{
		while(true)
		{
			yield return new WaitForEndOfFrame();
			animator.SetFloat(name, onValue);
		}
	}

	IEnumerator ResetFloatCoroutine(string name, float delay)
	{
		float currentValue = animator.GetFloat(name);
		while(currentValue > 0.0f)
		{
			yield return new WaitForSeconds(delay);
			currentValue = currentValue - decrement;
			if (currentValue < 0.0f) currentValue = 0.0f;
			animator.SetFloat(name, currentValue);
		}	
	}
}
