using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed.UI.Validator
{
    public class PageValidator : MonoBehaviour, IValidator
    {
        [SerializeField] Button validButton;
        IValidatable[] validatables;
        bool isValid;
        void Awake()
        {
            validButton.interactable = false;
            validatables = GetComponentsInChildren<IValidatable>();
            Log.Color("Validatables found in " + gameObject.name + ": " + validatables.Length, this);
            for (int i = 0; i < validatables.Length; i++)
            {
                validatables[i].Validator = this;
                Log.Color("Validatable " + i + ": " + validatables[i], this);
            }
        }
        public void Validate()
        {
            isValid = true;
            for (int i = 0; i < validatables.Length; i++)
            {
                isValid &= validatables[i].IsValid;
            }
            Log.Color("Is Valid: " + isValid, this);
            validButton.interactable = isValid;
        }
    }
}