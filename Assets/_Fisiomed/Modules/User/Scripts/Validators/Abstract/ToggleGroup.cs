using UnityEngine.UI;

namespace Fisiomed.UI.Validator
{
    public abstract class ToggleGroup : Validatable, IToggleGroup
    {        
        public virtual void Validate(string content)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
                OnInvalid(errorMessage);
            else
                OnValid();
            Validator.Validate();
        }
        public virtual void Validate(ExtensionsToggle toggle)
        {            
            Validate(toggle.UniqueID);
        }
    }
}
