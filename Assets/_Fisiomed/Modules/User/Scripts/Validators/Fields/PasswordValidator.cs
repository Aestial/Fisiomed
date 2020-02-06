using UnityEngine;

namespace Fisiomed.UI.Validator
{
    public class PasswordValidator : InputFieldValidator
    {
        [SerializeField] internal int minCharacters = 8;
        [SerializeField] internal string errorMessage;
        override public void Validate(string content)
        {
            base.Validate(content);
            if (IsValid)
            {
                if (content.Length < minCharacters)
                {
                    OnInvalid(errorMessage);
                    return;
                }
                OnValid();                
            }
        }        
    }
}