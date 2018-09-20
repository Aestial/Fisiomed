using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CharacterAnimation
{
	public AnimationClip clip;
	public float time;
}

[Serializable]
public struct CharacterReaction
{
	public CharacterAnimation face;
	public CharacterAnimation body;
}

[Serializable]
public struct Dialogue
{
	public string content;
	public float duration;
	public bool isLast;
	public CharacterReaction reaction;
}

[CreateAssetMenu]
public class DialogueInfo : ScriptableObject, ISerializationCallbackReceiver
{
	public Dialogue[] InitialValue;
	public Dialogue[] RuntimeValue;
	// private	List<Dialog> dialoglist;

	public void OnAfterDeserialize()
	{
		// this.RuntimeValue = this.InitialValue;
	}

	public void OnBeforeSerialize() {}
}
