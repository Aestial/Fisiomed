using System;

namespace Fisiomed.Quiz
{
	[Serializable]
	public class Quiz
	{
		public string title;
		public Question[] questions;
	}
	[Serializable]
	public class Question
	{
		public string text;
		public bool hasFeedback;
		public Answer[] answers;
	}
	[Serializable]
	public class Answer
	{
		public string text;
		public bool isCorrect;
		public string feedback;
	}
}
