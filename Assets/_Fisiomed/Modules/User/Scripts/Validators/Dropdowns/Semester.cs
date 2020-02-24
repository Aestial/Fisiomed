using UnityEngine;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class Semester : Dropdown
    {
        [SerializeField] NumberDropdownPopulator populator;
        override public void Validate(int option)
        {
            base.Validate(option);
            if (IsValid)
            {
                string semester = (option + populator.start - 1).ToString();
                UserManager.Instance.User.personal.semester = semester;                
            }                
        }
    }
}