using UnityEngine;

namespace Fisiomed.UI.Validator
{
    public abstract class ValidateFunction : MonoBehaviour
    {
        public virtual bool IsValid(string content)
        {
            return !(string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content));
        }
    }
}
