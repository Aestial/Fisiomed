using UnityEngine;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class Mail : InputField
    {        
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
                UserManager.Instance.User.email = content;
            }

        }        
    }
}
