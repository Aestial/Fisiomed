using UnityEngine;
using TMPro;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class UnamID : InputField
    {
        [SerializeField] PageValidator pageValidator;
        [SerializeField] TMP_Text textField;
        private void OnEnable()
        {
            pageValidator.Suscribe(this);
            UserManager.Instance.User.personal.unamID = textField.text;
        }
        private void OnDisable()
        {
            pageValidator.Unsuscribe(this);
            UserManager.Instance.User.personal.unamID = "";
        }
        override public void Validate(string content)
        {
            base.Validate(content);
            if (IsValid)
            {
                UserManager.Instance.User.personal.unamID = content;
            }            
        }
    }

}