using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace Fisiomed.UI.Validator
{
    [System.Serializable]
    public class StringVEvent : UnityEvent<string>
    {
    }
    [System.Serializable]
    public class StringStringVEvent : UnityEvent<string,string>
    {
    }
    public abstract class Validatable : MonoBehaviour, IValidatable
    {
        [SerializeField] internal string key = default;
        [SerializeField] internal string errorMessage = default;
        [SerializeField] internal TMP_Text message = default;
        [SerializeField] internal bool getErrorStringFromText = false;        
        [SerializeField] StringStringVEvent onValid = default;
        [SerializeField] UnityEvent onInvalid = default;
        public Validator validator;

        private bool _isValid = false;
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;                
                message.enabled = !value;
                validator.Validate();
            }
        }
        public virtual void Start()
        {
            if (!string.IsNullOrEmpty(message.text) && getErrorStringFromText)
                errorMessage = message.text;         
            message.enabled = false;            
        }
        private void OnEnable()
        {
            validator.Subscribe(this);            
        }
        private void OnDisable()
        {
            validator.Unsubscribe(this);            
        }
        public void SetValid(string content)
        {
            IsValid = true;
            onValid.Invoke(content, key);
        }
        public void SetInvalid(string error)
        {
            message.text = error;
            IsValid = false;            
            onInvalid.Invoke();
        }
    }
}