using UnityEngine;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class NameValidator : InputFieldValidator
    {
        [SerializeField] bool isSurname;
        override public void Validate(string content)
        {
            base.Validate(content);
            if (IsValid)
            {
                if (isSurname)
                    UserManager.Instance.user.personal.surname = content;
                else 
                    UserManager.Instance.user.personal.name = content;
                UserManager.Instance.Save();
            }
        }
    }
}