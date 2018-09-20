using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed.Auth
{
	[RequireComponent(typeof(CanvasGroup))]
	public class EnterPanelController : MonoBehaviour
	{
		[SerializeField] bool showInAwake;
		[SerializeField] private EnterInputField[] inputFields;
		[SerializeField] Button actionButton;
		public MessageController mc;

		private CanvasGroup canvasGroup;
		private bool isActive;

		// Use this for initialization
		void Awake ()
		{
			this.canvasGroup = this.GetComponent<CanvasGroup>();
			this.isActive = this.AnimateCanvasGroup(this.showInAwake);
			this.ActiveActionButton();
		}
		
        public void ActiveActionButton()
        {
            this.actionButton.interactable = AreFieldsValid();
        }

		public void ActiveCanvasGroup(bool active)
		{
			this.AnimateCanvasGroup(active);
		}

        private bool AnimateCanvasGroup(bool active)
		{
			this.canvasGroup.interactable = active;
			this.canvasGroup.blocksRaycasts = active;
			this.canvasGroup.alpha = active ? 1.0f : 0.0f;

			return this.canvasGroup.interactable;
		}

		private bool AreFieldsValid()
		{
			bool valid = true;
			for (int i = 0; i < inputFields.Length; i++)
			{
				valid = inputFields[i].isValid && valid;
			}
			return valid;
		}
	
	}
}
