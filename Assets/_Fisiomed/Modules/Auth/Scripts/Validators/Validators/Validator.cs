using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Fisiomed.UI.Validator
{
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }
    public class Validator : MonoBehaviour, IValidator
    {
        [SerializeField] BoolEvent onValidated = default;
        readonly List<Validatable> validatables = new List<Validatable>();

        void Start()
        {
            for (int i = 0; i < validatables.Count; i++)
            {
                validatables[i].validator = this;
                Log.Color("Validatable " + i + ": " + validatables[i], this);
            }
        }
        public void Subscribe(Validatable validatable)
        {
            validatables.Add(validatable);
        }
        public void Unsubscribe(Validatable validatable)
        {
            validatables.Remove(validatable);
        }
        public void Validate()
        {
            bool isValid = true;
            for (int i = 0; i < validatables.Count; i++)
            {
                isValid &= validatables[i].IsValid;
            }
            onValidated.Invoke(isValid);
            Log.Color(gameObject.name + " validated: " + isValid, this);
        }
    }
}