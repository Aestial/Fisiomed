using UnityEngine;
using TMPro;

namespace Fisiomed.UI.Validator
{
    public class PasswordConfirmValidateFunction : ValidateFunction
    {
        [SerializeField] private TMP_InputField OtherInputField;

        override public bool IsValid(string content)
        {
            return content == OtherInputField.text;
        }
    }
}