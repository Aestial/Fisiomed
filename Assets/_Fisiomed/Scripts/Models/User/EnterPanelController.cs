using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed
{
	[RequireComponent(typeof(CanvasGroup))]
	public class EnterPanelController : MonoBehaviour
	{
		[SerializeField] bool showInAwake;
		[SerializeField] private EnterInputField[] inputFields;
		[SerializeField] Button actionButton;
		[SerializeField] private MessageController mc;

		private CanvasGroup canvasGroup;
		private bool isActive;

		// Use this for initialization
		void Awake ()
		{
			this.canvasGroup = this.GetComponent<CanvasGroup>();
			this.isActive = this.ActiveCanvasGroup(this.showInAwake);
			this.ActiveActionButton();
		}

        // Update is called once per frame
        void Update()
        {

        }

        public void ActiveActionButton()
        {
            this.actionButton.interactable = AreFieldsValid();
        }

        private bool ActiveCanvasGroup(bool active)
		{
			this.canvasGroup.interactable = active;
			this.canvasGroup.blocksRaycasts = active;
			this.canvasGroup.alpha = active ? this.canvasGroup.alpha : 0.0f;

			return this.canvasGroup.interactable;
		}

		private bool AreFieldsValid()
		{
			bool valid = true;
			for (int i = 0; i < inputFields.Length; i++)
			{
				valid = inputFields[i].IsValid && valid;
                if (!valid)
                {
                    switch(inputFields[i].type)
                    {
                        case EnterInputField.Type.email:
                            this.mc.Display("Introduce un correo válido.");
                            break;
                        case EnterInputField.Type.username:
                            this.mc.Display("Introduce un nombre de usuario.");
                            break;
                        case EnterInputField.Type.password:
                            this.mc.Display("Introduce una contraseña con 6 digitos o más.");
                            break;
                        default:
                            return false;
                    }
                }
			}
			return valid;
		}
	}
}
