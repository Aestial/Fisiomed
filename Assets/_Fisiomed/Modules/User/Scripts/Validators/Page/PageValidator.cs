using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed.UI.Validator
{
    public class PageValidator : MonoBehaviour
    {
        [SerializeField] Button validButton;
        IValidator[] validators;
        bool isValid;
        void Start()
        {
            validButton.interactable = false;
            validators = GetComponentsInChildren<IValidator>();
            Debug.Log("Validators found:" + validators.Length);
            for (int i = 0; i < validators.Length; i++)
            {
                validators[i].Page = this;
            }
        }
        public void Validate()
        {
            isValid = true;
            for (int i = 0; i < validators.Length; i++)
            {
                isValid &= validators[i].IsValid;
            }
            validButton.interactable = isValid;
        }
    }
}