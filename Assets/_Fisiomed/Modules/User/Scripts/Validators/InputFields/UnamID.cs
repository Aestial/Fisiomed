using UnityEngine;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class UnamID : InputField
    {
        override public void Start()
        {
            base.Start();
            gameObject.SetActive(false);
        }
        override public void Validate(string content)
        {
            base.Validate(content);
            if (IsValid)
            {
                UserManager.Instance.User.personal.unamID = content;
            }            
        }
    }

}