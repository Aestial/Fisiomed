using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Fisiomed
{
	public class EnterInputField : MonoBehaviour 
	{
		public enum Type
		{
			email,
			username,
			password
		}
		public Type type;
		public TMP_InputField inputField;
		bool isValid;
		public bool IsValid
		{
			get 
			{
				string text = inputField.text;
				isValid = CheckValid(text);
				if (isValid)
					Debug.Log("<color=green>"+ text + "</color>");
				else
					Debug.Log("<color=red>"+ text + "</color>");
				return isValid;
			}
			set {isValid = value;}
		}

		public void VerifyValid(string value)
		{
			
		}

		public bool CheckValid(string value)
		{
			switch(type)
			{
				case Type.email:
					var addr = new System.Net.Mail.MailAddress(value);
					return (addr.Address == value) && (value.Length >= 5);
				case Type.username:
					return value != "";
				case Type.password:
					return value.Length >= 6;
				default:
					return false;
			}
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}
