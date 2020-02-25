using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed.UI.Validator
{
    public class PageValidator : MonoBehaviour, IValidator
    {
        [SerializeField] Button validButton;
        List<IValidatable> validatables = new List<IValidatable>();
        bool isValid;
        void Start()
        {
            validButton.interactable = false;
            IValidatable[] vs = GetComponentsInChildren<IValidatable>();            
            Log.Color("Validatables found in " + gameObject.name + ": " + vs.Length, this);
            validatables = new List<IValidatable>(vs);
            for (int i = 0; i < validatables.Count; i++)
            {
                validatables[i].Validator = this;
                Log.Color("Validatable " + i + ": " + validatables[i], this);
            }
        }
        public void Suscribe(IValidatable validatable)
        {
            validatable.Validator = this;
            validatables.Add(validatable);            
        }
        public void Unsuscribe(IValidatable validatable)
        {
            validatable.Validator = null;
            validatables.Remove(validatable);
        }
        public void Validate()
        {
            isValid = true;
            for (int i = 0; i < validatables.Count; i++)
            {
                isValid &= validatables[i].IsValid;
            }
            Log.Color("Is Valid: " + isValid, this);
            validButton.interactable = isValid;
        }
    }
}