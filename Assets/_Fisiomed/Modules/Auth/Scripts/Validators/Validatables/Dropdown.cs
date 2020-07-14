using UnityEngine;

namespace Fisiomed.UI.Validator
{
    public class Dropdown : Validatable, IDropdown
    {
        [SerializeField] internal int startOption = 1;
        [SerializeField] internal DropdownPopulator dropdownPopulator = default;

        public void Validate(int option)
        {
            if (option < startOption)
            {
                SetInvalid(errorMessage);
                return;
            }
            int value = option + dropdownPopulator.startValue - 1;
            SetValid(value.ToString());            
        }        
    }
}

