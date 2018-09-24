using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerController : MonoBehaviour 
{
	public QuestionController question;
	public bool isCorrect;
	[SerializeField] private TMP_Text text;

	public void Set(QuestionController question, Answer answer)
	{
		this.question = question;
		this.isCorrect = answer.value;
		this.text.text = answer.text;
	}
	
	public void Check ()
	{
		if (this.isCorrect)
			this.question.CorrectAnswer();
	}
}