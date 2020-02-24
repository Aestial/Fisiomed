using UnityEngine;

namespace Fisiomed.UI.Validator
{
    public class Password : InputField
    {
        [SerializeField] internal int minCharacters = 8;
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