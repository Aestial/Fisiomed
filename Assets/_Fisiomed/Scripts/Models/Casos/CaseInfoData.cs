using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct CaseUnit
{
	public string name;
	public int index;
}

[Serializable]
public struct CaseInfo
{
	public string name;
	public CaseUnit unit;
	public string theme;
	public string skill;
	public string goal;
	public string gameplay;
}

[CreateAssetMenu]
public class CaseInfoData : ScriptableObject, ISerializationCallbackReceiver
{
	public CaseInfo InitialValue;
	public CaseInfo RuntimeValue;
	// private	List<Dialog> dialoglist;

	public void OnAfterDeserialize()
	{
		// this.RuntimeValue = this.InitialValue;
	}

	public void OnBeforeSerialize() {}
}

