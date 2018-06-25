using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{
	[SerializeField] private DialogueInfo dialogueInfo;
	[SerializeField] private Text text;
	[SerializeField] private float delay;
	[SerializeField] private bool showFirstOnAwake;
	[SerializeField] private bool playOnAwake;

	[SerializeField] private Button backButton;
	[SerializeField] private Button nextButton;

	private Dialogue[] dialogues;
	private int last;
	private int index;

	void Awake ()
	{
		// Retrieve dialogs data from ScriptableObject
		this.dialogues = this.dialogueInfo.InitialValue;
		// Initialize length, index and text
		this.last = this.dialogues.Length - 1;
		this.index = 0;
		this.text.text = "";

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

	void Start()
	{
		this.backButton.onClick.AddListener(this.Back);
		this.nextButton.onClick.AddListener(this.Next);
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

	private void UpdateText (Dialogue dialogue)
	{
		this.text.text = dialogue.content;
	}

	private void UpdateButtons (int index)
	{
		this.backButton.interactable = true;
		this.nextButton.interactable = true;
		if (index == 0)
			this.backButton.interactable = false;
		else if (index == this.last)
			this.nextButton.interactable = false;
	}

	private void PlayAll ()
	{
		StartCoroutine(this.PlayAllCoroutine());
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
		this.backButton.onClick.RemoveListener(this.Back);
		this.nextButton.onClick.RemoveListener(this.Next);
	}
}
