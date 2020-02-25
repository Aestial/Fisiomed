using UnityEngine.UI;
using Fisiomed.User;

namespace Fisiomed.UI.Validator
{
    public class University : ToggleGroup
    {
        override public void Validate(ExtensionsToggle toggle)
        {
            base.Validate(toggle.UniqueID);
            if (IsValid)
            {
                string uni = toggle.UniqueID;
                UserManager.Instance.User.personal.university = uni;
            }
        }
    }
}
