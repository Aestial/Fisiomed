using UnityEngine;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class SemesterValidator : DropdownValidator
    {
        [SerializeField] NumberDropdownPopulator populator;
        override public void Validate(int option)
        {
            base.Validate(option);
            if (IsValid)
            {
                string semester = (option + populator.start - 1).ToString();
                UserManager.Instance.user.personal.semester = semester;
                UserManager.Instance.Save();
            }                
        }
    }
}