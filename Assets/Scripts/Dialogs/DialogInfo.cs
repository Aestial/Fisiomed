using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Dialog
{
	public string content;
	public float duration;
	public bool isLast;
}

[CreateAssetMenu]
public class DialogInfo : ScriptableObject, ISerializationCallbackReceiver
{
	public Dialog[] InitialValue;
	public Dialog[] RuntimeValue;
	// private	List<Dialog> dialoglist;

	public void OnAfterDeserialize()
	{
		// this.RuntimeValue = this.InitialValue;
	}

	public void OnBeforeSerialize() {}
}
