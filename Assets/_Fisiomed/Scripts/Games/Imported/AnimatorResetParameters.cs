using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorResetParameters : MonoBehaviour 
{
	Animator animator;

	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	public void ResetAll () 
	{
		for (int i = 0; i < animator.parameterCount; i++)
		{
			var p = animator.parameters[i];
			if (p.type == AnimatorControllerParameterType.Trigger)
			{
				animator.ResetTrigger(p.name);
			}
		}
	}
}
