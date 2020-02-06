using UnityEngine;
using TMPro;

namespace Fisiomed.UI.Validator
{
    public class UnamIDValidator : MonoBehaviour, IInputFieldValidator
    {
        [SerializeField] private TMP_Text message;
        [SerializeField] private string emptyErrorMessage;
        public PageValidator Page { private get; set; }
        public bool IsValid { get; private set; }
        void Start()
        {
            message.enabled = false;
        }
        public void Validate(string content)
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