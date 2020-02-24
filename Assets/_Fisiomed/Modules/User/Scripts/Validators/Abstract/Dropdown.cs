using UnityEngine;

namespace Fisiomed.UI.Validator
{
    public abstract class Dropdown : Validatable, IDropdown
    {
        [SerializeField] internal int startOption;
        public virtual void Validate(int option)
        {
            if (option < startOption)
                OnInvalid(errorMessage);
            else
                OnValid();
            Validator.Validate();
        }        
    }
}

