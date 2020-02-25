using UnityEngine;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class Name : InputField
    {
        [SerializeField] bool isSurname;
        override public void Validate(string content)
        {
            base.Validate(content);
            if (IsValid)
            {
                if (isSurname)
                    UserManager.Instance.User.personal.surname = content;
                else 
                    UserManager.Instance.User.personal.name = content;                
            }
        }
    }
}