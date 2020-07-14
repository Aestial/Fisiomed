using UnityEngine;

namespace Fisiomed.UI.Validator
{
    public class InputField : Validatable, IInputField
    {
        private ValidateFunction validateFunction;

        public override void Start()
        {
            base.Start();
            validateFunction = GetComponent<ValidateFunction>();            
        }

        public void Validate(string content)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
            {
                SetInvalid(errorMessage);
                return;
            }
            if (validateFunction != null && !validateFunction.IsValid(content))
            {
                SetInvalid(errorMessage);
                return;
            }
            SetValid(content);
        }
    }
}