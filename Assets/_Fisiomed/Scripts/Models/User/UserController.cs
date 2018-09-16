using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour 
{
	public User myUser;

	private AuthHandler auth;
	
	// Use this for initialization
	void Awake () 
	{
		myUser = new User();
		auth = GetComponent<AuthHandler>();
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SignUp()
	{
		auth.CreateUserWithEmailAsync();
	}

	public bool ChangeValue(User.UserValue type, string value)
	{
		switch(type)
		{
			case User.UserValue.Name:
				myUser.name = value;
				Debug.Log(myUser.name);
				break;
			case User.UserValue.Email:
				myUser.email = value;
				Debug.Log(myUser.email);
				break;
			case User.UserValue.Password:
				myUser.password = value;
				Debug.Log(myUser.password);
				break;
			default:
				break;
		}
		return true;
	}

}
