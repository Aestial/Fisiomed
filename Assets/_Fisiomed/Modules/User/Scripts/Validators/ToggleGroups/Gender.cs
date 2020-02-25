using UnityEngine.UI;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class Gender : ToggleGroup
    {
        override public void Validate(ExtensionsToggle toggle)
        {
            base.Validate(toggle.UniqueID);
            if (IsValid)
            {
                string gender = toggle.UniqueID;
                UserManager.Instance.User.personal.gender = gender;
            }
        }
    }
}
