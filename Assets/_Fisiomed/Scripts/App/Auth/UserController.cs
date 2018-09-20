using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed.Auth
{
	public class UserController : MonoBehaviour 
	{
		[SerializeField] EnterPanelController logInPanel;
		[SerializeField] EnterPanelController signUpPanel;
		[SerializeField] float screenDelay;

		public User myUser;
		private AuthHandler auth;
		private bool isSignUp;
		
		// Use this for initialization
		void Awake () 
		{
			myUser = new User();
			auth = GetComponent<AuthHandler>();
		}

		public void ChangeScreen(bool isSignUP)
		{
			this.isSignUp = isSignUP;
			this.logInPanel.ActiveCanvasGroup(!isSignUP);
			this.signUpPanel.ActiveCanvasGroup(isSignUP);
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
			auth.CreateUserWithEmailAsync();
		}

		public void LogIn()
		{
			auth.SigninWithEmailAsync();
		}

		public void HandleLogIn()
		{
			this.DisplayMessage("¡Bienvenido!");
			PlayerPrefs.SetString("Username", this.myUser.name);
			PlayerPrefs.SetString("UserRange", this.myUser.range);
			AppManager.Instance.ChangeScene("MainScene");
		}

		public bool ChangeValue(EnterInputField.Type type, string value)
		{
			switch(type)
			{
				case EnterInputField.Type.Username:
					myUser.name = value;
					Debug.Log(myUser.name);
					break;
				case EnterInputField.Type.Email:
					myUser.email = value;
					Debug.Log(myUser.email);
					break;
				case EnterInputField.Type.Password:
					myUser.password = value;
					Debug.Log(myUser.password);
					break;
				default:
					break;
			}
			return true;
		}

		public void DisplayMessage(string message)
		{
			if (this.isSignUp)
				this.signUpPanel.mc.Display(message);
			else
				this.logInPanel.mc.Display(message);
		}

	}

}
