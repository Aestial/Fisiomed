using UnityEngine;
using TMPro;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class PasswordConfirmValidator : PasswordValidator
    {
        [SerializeField] private TMP_InputField OtherInputField;
        override public void Validate(string content)
        {
            base.Validate(content);
            if(IsValid)
            {
                if (content != OtherInputField.text)
                {
                    OnInvalid(errorMessage);
                    return;
                }
                OnValid();
                UserManager.Instance.user.password = content;
            }            
        }
    }
}