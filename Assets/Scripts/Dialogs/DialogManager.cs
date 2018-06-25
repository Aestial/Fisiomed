using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour 
{
	[SerializeField] private DialogInfo dialogInfo;
	[SerializeField] private Text text;
	[SerializeField] private float delay;
	[SerializeField] private bool playOnAwake;

	private Dialog[] dialogs;

	void Awake ()
	{
		this.dialogs = this.dialogInfo.InitialValue;
		this.text.text = "";
		if (this.playOnAwake)
		{
			this.Play();
		}
	}

	void Play ()
	{
		StartCoroutine(this.PlayCoroutine());
	}

	private IEnumerator PlayCoroutine()
	{
		yield return new WaitForSeconds(this.delay);
		for (int i = 0; i < this.dialogs.Length; i++)
		{
			Dialog dialog = this.dialogs[i];
			this.text.text = dialog.content;
			yield return new WaitForSeconds(dialog.duration);
		}
	}
}
