using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueAnimator : MonoBehaviour 
{
	[SerializeField] private float frequency = default;

	private TMP_Text m_TextComponent;
	private string m_Text;
	private string m_DisplayText;

	private float m_WaitTime;
	private int m_Index;

	// Callback event && delegate
	// public delegate

	void Start () 
	{
		this.m_TextComponent = GetComponent<TMP_Text>();
	}

	public void Play()
	{
		this.m_Text = this.m_TextComponent.text;
		this.m_Index = 0;
		StartCoroutine(this.AnimateCoroutine());
	}

	private IEnumerator AnimateCoroutine()
	{
		while (this.m_Index <= this.m_Text.Length)
		{
			this.m_WaitTime = (this.frequency <= 0.0f) ? 0.0f : (1.0f / this.frequency);
			this.m_DisplayText = this.m_Text.Substring(0, this.m_Index);
			this.m_TextComponent.text = this.m_DisplayText;
			this.m_Index++;
			yield return new WaitForSeconds(this.m_WaitTime);
		}
		// Trigger callback
	}
}
