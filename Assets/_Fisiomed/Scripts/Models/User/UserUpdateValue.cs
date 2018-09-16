using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserUpdateValue : MonoBehaviour 
{
	[SerializeField] UserController userController;
	[SerializeField] private User.UserValue valueType;

	public void UpdateClassValue (string str)
	{
		userController.ChangeValue(valueType, str);
	}
}
