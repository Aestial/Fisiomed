using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Case
{
	None,	
	Insomnio,
	Epilepsia,
}

[CreateAssetMenu]
public class CurrentCaseVariable : ScriptableObject, ISerializationCallbackReceiver 
{
	public Case InitialValue;
	public Case m_RuntimeValue;

	public void OnAfterDeserialize()
	{
		this.m_RuntimeValue = this.InitialValue;
	}

	public void OnBeforeSerialize()
	{

	}
}
