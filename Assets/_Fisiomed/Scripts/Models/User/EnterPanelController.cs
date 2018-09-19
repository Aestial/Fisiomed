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
		[SerializeField] private MessageController message;

		private CanvasGroup canvasGroup;
		private bool isActive;

		// Use this for initialization
		void Awake ()
		{
			this.canvasGroup = this.GetComponent<CanvasGroup>();
			this.isActive = this.ActiveCanvasGroup(this.showInAwake);
			this.ActiveActionButton();
		}

		bool ActiveCanvasGroup(bool active)
		{
			this.canvasGroup.interactable = active;
			this.canvasGroup.blocksRaycasts = active;
			this.canvasGroup.alpha = active ? this.canvasGroup.alpha : 0.0f;

			return this.canvasGroup.interactable;
		}

		public void ActiveActionButton()
		{
			this.actionButton.interactable = AreFieldsValid();
		}

		bool AreFieldsValid()
		{
			bool active = true;
			for (int i = 0; i < inputFields.Length; i++)
			{
				active = inputFields[i].IsValid && active;
			}
			return active;
		}
		// Update is called once per frame
		void Update ()
		{

		}
	}
}
