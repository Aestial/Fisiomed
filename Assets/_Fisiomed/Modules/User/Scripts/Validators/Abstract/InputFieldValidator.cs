using UnityEngine;
using TMPro;

namespace Fisiomed.UI.Validator
{
    public abstract class InputFieldValidator : MonoBehaviour, IInputFieldValidator
    {
        [SerializeField] internal TMP_Text message;
        [SerializeField] internal string emptyErrorMessage;
        public PageValidator Page { private get; set; }
        public bool IsValid { get; private set; }
        public void Start()
        {
            message.enabled = false;
        }
        public virtual void Validate(string content)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
            {
                OnInvalid(emptyErrorMessage);
                return;
            }            
            OnValid();
        }
        public void OnValid()
        {
            IsValid = true;
            message.enabled = false;
            Page.Validate();
        }
        public void OnInvalid(string error)
        {
            IsValid = false;
            message.enabled = true;
            message.text = error;
        }
    }
}