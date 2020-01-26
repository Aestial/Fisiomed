using UnityEngine;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class AgeValidator : DropdownValidator
    {
        [SerializeField] NumberDropdownPopulator populator;
        override public void Validate(int option)
        {
            base.Validate(option);
            if (IsValid)
            {
                string age = (option + populator.start - 1).ToString();
                UserManager.Instance.user.personal.age = age;
                UserManager.Instance.Save();
            }                
        }        
    }
}