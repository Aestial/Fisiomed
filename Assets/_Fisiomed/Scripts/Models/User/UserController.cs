using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UserController : MonoBehaviour 
{
	[SerializeField] MessageController message;
	// ___
	[SerializeField] GameObject logInScreen;
	[SerializeField] GameObject signUpScreen;
	[SerializeField] float screenDelay;

	public User myUser;
	private bool isValidEmail = false;
	private bool isValidName = false;
	private bool isValidPassword = false;
	private AuthHandler auth;
	
	// Use this for initialization
	void Awake () 
	{
		myUser = new User();
		auth = GetComponent<AuthHandler>();
	}

	void Start ()
	{
		ChangeScreen(true);
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void ChangeScreen(bool isSignUP)
	{
		this.logInScreen.SetActive(!isSignUP);
		this.signUpScreen.SetActive(isSignUP);
	}

	// THIS IS SHIT _____
	public void ChangeScreenDelay(bool isSignUP)
	{
		StartCoroutine(ChangeScreenCoroutine(isSignUP));
	}
	private IEnumerator ChangeScreenCoroutine(bool isSignUP)
	{
		yield return new WaitForSeconds(screenDelay);
		ChangeScreen(isSignUP);
	}

	public void SignUp()
	{
		if (isValidEmail && isValidName && isValidPassword)
			auth.CreateUserWithEmailAsync();
		if (!isValidName)
			DisplayMessage("Introduce un nombre de usuario.");
		if (!isValidEmail)
			DisplayMessage("Introduce un correo válido.");
		if (!isValidPassword)
			DisplayMessage("Introduce una contraseña con 6 digitos o más.");
	}

	public bool ChangeValue(User.UserValue type, string value)
	{
		this.message.Deactivate();
		switch(type)
		{
			case User.UserValue.Name:
				isValidName = IsValidName(value);
				if (isValidName) {
					myUser.name = value;
					Debug.Log(myUser.name);
				} else
					DisplayMessage("Introduce un nombre de usuario.");
				break;
			case User.UserValue.Email:
				isValidEmail = IsValidEmail(value);
				if(isValidEmail) {
					myUser.email = value;
					Debug.Log(myUser.email);
				} else
					DisplayMessage("Introduce un correo válido.");			
				break;
			case User.UserValue.Password:
				isValidPassword = IsValidPassword(value);
				if(isValidPassword) {
					myUser.password = value;
					Debug.Log(myUser.password);
				} else
					DisplayMessage("Introduce una contraseña con 6 digitos o más.");
				break;
			default:
				break;
		}
		return true;
	}

	private bool IsValidName(string value)
	{
		return value != "";
	}

	private bool IsValidPassword(string value)
	{
		return value.Length >= 6;
	}

	private bool IsValidEmail(string value)
	{
		try {
			var addr = new System.Net.Mail.MailAddress(value);
			return addr.Address == value;
		}
		catch {
			return false;
		}
	}

	public void DisplayMessage(string message)
	{
		this.message.Display(message);
	}

}
