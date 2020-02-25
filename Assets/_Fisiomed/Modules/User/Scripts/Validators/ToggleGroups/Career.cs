using UnityEngine.UI;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class Career : ToggleGroup
    {
        override public void Validate(ExtensionsToggle toggle)
        {
            base.Validate(toggle.UniqueID);
            if (IsValid)
            {
                string career = toggle.UniqueID;
                UserManager.Instance.User.personal.career = career;
            }
        }
        override public void Validate(string content)
        {
            base.Validate(content);
            if (IsValid)
            {                
                UserManager.Instance.User.personal.career = content;
            }
        }
    }
}
