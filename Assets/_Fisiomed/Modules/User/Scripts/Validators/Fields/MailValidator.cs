using UnityEngine;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class MailValidator : InputFieldValidator
    {
        [SerializeField] private string errorMessage;
        override public void Validate(string content)
        {
            base.Validate(content);
            if(IsValid)
            {
                if (!RegexUtilities.IsValidEmail(content))
                {
                    OnInvalid(errorMessage);
                    return;
                }
                OnValid();
                UserManager.Instance.user.email = content;
            }

        }        
    }
}
