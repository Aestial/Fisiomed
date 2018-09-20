using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Fisiomed.Auth
{
	public class EnterInputField : MonoBehaviour
	{
		public enum Type
		{
			Email,
			Username,
			Password
		}
        public Type type;
        [HideInInspector] public bool isValid;
		
		[SerializeField] private TMP_InputField inputField;
		[SerializeField] private MessageController mc;
		[SerializeField] private string errorMessage;

		// DECOUPLE PLEASE
		[SerializeField] private UserController user;

        public void VerifyValid(string value)
		{
			this.isValid = CheckValid(this.type, value);
			if (this.isValid) {
				user.ChangeValue(this.type, value);
				mc.Deactivate();
				Debug.Log("<color=green>Valid "+ this.type + ": " +  value + "</color>");
			} else {
				mc.Display(this.errorMessage);
				Debug.Log("<color=orange>Invalid "+ this.type + ": " +  value + "</color>");
			}
		}

		private bool CheckValid(Type type, string value)
		{
			switch(type) {
				case Type.Email:
					return value.Length >= 5;
				case Type.Username:
					return value != "";
				case Type.Password:
					return value.Length >= 6;
				default:
					return false;
			}
		}
	}
}
