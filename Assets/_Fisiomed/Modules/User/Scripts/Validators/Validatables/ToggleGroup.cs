using UnityEngine.UI;

namespace Fisiomed.UI.Validator
{
    public class ToggleGroup : Validatable, IToggleGroup
    {        
        public void Validate(string content)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
            {
                SetInvalid(errorMessage);
                return;
            }
            SetValid(content);            
        }
        public void Validate(ExtensionsToggle toggle)
        {            
            Validate(toggle.UniqueID);
        }
    }
}
