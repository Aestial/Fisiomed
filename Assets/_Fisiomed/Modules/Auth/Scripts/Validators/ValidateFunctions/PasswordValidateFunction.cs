using UnityEngine;

namespace Fisiomed.UI.Validator
{
    public class PasswordValidateFunction : ValidateFunction
    {
        [SerializeField] internal int minCharacters = 8;

        override public bool IsValid(string content)
        {
            return content.Length >= minCharacters;
        }        
    }
}