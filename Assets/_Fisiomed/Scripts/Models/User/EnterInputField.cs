using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Fisiomed
{
	public class EnterInputField : MonoBehaviour
	{
        [SerializeField] private TMP_InputField inputField;

        public enum Type
		{
			email,
			username,
			password
		}
        public Type type;

        private MessageController mc;
		private bool isValid;

		public bool IsValid
		{
			get
			{
				this.VerifyValid(inputField.text);
				return isValid;
			}
			set {isValid = value;}
		}

        // Use this for initialization
        void Start()
        {
            this.mc = FindObjectOfType<MessageController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void VerifyValid(string value)
		{
			isValid = CheckValid(value);
			if (isValid)
				Debug.Log("<color=green>"+ value + "</color>");
			else
				Debug.Log("<color=red>"+ value + "</color>");
		}

		private bool CheckValid(string value)
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
	}
}
