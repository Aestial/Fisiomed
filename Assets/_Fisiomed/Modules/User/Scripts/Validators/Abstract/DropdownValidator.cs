using UnityEngine;
using TMPro;

namespace Fisiomed.UI.Validator
{
    public abstract class DropdownValidator : MonoBehaviour, IDropdownValidator
    {
        [SerializeField] internal TMP_Text message;
        [SerializeField] internal string errorMessage;
        [SerializeField] internal int startOption;
        public PageValidator Page { private get; set; }
        public bool IsValid { get; private set; }
        public void Start()
        {
            message.enabled = false;
        }
        public virtual void Validate(int option)
        {
            if (option < startOption)
            {
                OnInvalid(errorMessage);
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

