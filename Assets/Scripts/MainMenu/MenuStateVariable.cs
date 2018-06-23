using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
	Splash,
	Loading,
	LogMenu,
	LogIn,
	SignUp,
	TopMenu,
	Progress,
	Settings,
	Profile,
	Volume,
}

[CreateAssetMenu]
public class MenuStateVariable : ScriptableObject,
ISerializationCallbackReceiver
{
	public MenuState InitialValue;

	private MenuState m_RuntimeValue;

	public MenuState RuntimeValue
	{
		get { return m_RuntimeValue; }
		set { this.SetState(value); }
	}

	public delegate void StateChangedAction(MenuState value);
	public event StateChangedAction OnStateChanged;

	public void OnAfterDeserialize()
	{
		// this.RuntimeValue = this.InitialValue;
		this.SetState(this.InitialValue);
	}

	public void OnBeforeSerialize() {}

	private void SetState(MenuState value)
	{
		this.m_RuntimeValue = value;
		if (this.OnStateChanged != null)
			this.OnStateChanged(value);
	}
}
