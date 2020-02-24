using UnityEngine;
using TMPro;

namespace Fisiomed.UI.Validator
{
    public abstract class Validatable : MonoBehaviour, IValidatable
    {
        [SerializeField] internal TMP_Text message;
        [SerializeField] internal string errorMessage;
        public IValidator Validator { internal get; set; }
        public bool IsValid { get; private set; }
        public virtual void Start()
        {
            message.enabled = false;
        }
        public void OnValid()
        {
            IsValid = true;
            message.enabled = false;
        }
        public void OnInvalid(string error)
        {
            IsValid = false;
            message.enabled = true;
            message.text = error;
        }
    }
}