using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour 
{
	[SerializeField] private DialogueInfo dialogueInfo;
	[SerializeField] private TMP_Text text;
	[SerializeField] private float delay;
	[SerializeField] private bool showFirstOnAwake;
	[SerializeField] private bool playOnAwake;

	[SerializeField] private Button backButton;
	[SerializeField] private Button nextButton;

	[SerializeField] private DialogueAnimator animator;

	private Dialogue[] dialogues;
	private int last;
	private int index;

    IEnumerator Start ()
	{
		// Retrieve dialogs data from ScriptableObject
		this.dialogues = this.dialogueInfo.InitialValue;
		// Initialize length, index and text
		this.last = this.dialogues.Length - 1;
		this.index = 0;
		this.text.text = "";

        yield return new WaitForEndOfFrame();
		if (this.showFirstOnAwake)
		{
			Dialogue dialogue = this.dialogues[this.index];
			this.UpdateText(dialogue);
			this.UpdateButtons(this.index);
		}
		
		if (this.playOnAwake)
		{
			this.PlayAll();
		}

	}

	void Awake()
	{
		this.SubscribeButtons(true);
	}
	
	public void Next ()
	{
		if (this.index < this.last)
		{
			this.index++;
			Dialogue dialogue = this.dialogues[this.index];
			this.UpdateText(dialogue);
			this.UpdateButtons(this.index);
		}
	}

	public void Back ()
	{
		if (this.index > 0)
		{
			this.index--;
			Dialogue dialogue = this.dialogues[this.index];
			this.UpdateText(dialogue);
			this.UpdateButtons(this.index);
		}		
	}

	private void PlayAll ()
	{
		StartCoroutine(this.PlayAllCoroutine());
	}

	private void UpdateText (Dialogue dialogue)
	{
		this.text.text = dialogue.content;
		this.animator.Play();
	}

	private void UpdateButtons (int index)
	{
		if (this.backButton != null)
		{
			this.backButton.interactable = true;
			if (index == 0)
				this.backButton.interactable = false;
		}
		if (this.nextButton != null)
		{
			this.nextButton.interactable = true;
			if (index == this.last)
				this.nextButton.interactable = false;
		}
	}

	private void SubscribeButtons(bool on)
	{
		if (this.backButton != null && this.nextButton != null)
		{
			if (on)
			{
				this.backButton.onClick.AddListener(this.Back);
				this.nextButton.onClick.AddListener(this.Next);
			}
			else 
			{
				this.backButton.onClick.RemoveListener(this.Back);
				this.nextButton.onClick.RemoveListener(this.Next);
			}
		}
	}

	private IEnumerator PlayAllCoroutine()
	{
		yield return new WaitForSeconds(this.delay);
		for (int i = 0; i < this.dialogues.Length; i++)
		{
			this.index = i;
			Dialogue dialog = this.dialogues[this.index];
			this.UpdateText(dialog);
			yield return new WaitForSeconds(dialog.duration);
		}
	}

	void OnDestroy()
	{
		this.SubscribeButtons(false);	
	}
}
