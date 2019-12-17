using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Fisiomed.Feedback;

namespace Fisiomed.Chat
{
    public class AnswerController : MonoBehaviour
    {
        public QuestionController question;
        [SerializeField] TMP_Text text;
        [SerializeField] Image background;
        [SerializeField] float continueWaitTime;
        [SerializeField] UnityEvent correctEvents;
        [SerializeField] UnityEvent wrongEvents;
        Answer answer;
        Button button;
        bool notAnswered;
        public void Set(QuestionController question, Answer answer, Character character)
        {
            this.question = question;
            this.answer = answer;
            text.text = answer.text;
        }
        void SetColors(string text, string textB, string faceB)
        {
            Color textColor = new Color();
			Color textBColor = new Color();
			ColorUtility.TryParseHtmlString (text, out textColor);
			ColorUtility.TryParseHtmlString (textB, out textBColor);
			SetColors(textColor, textBColor);
        }
        void SetColors(Color textColor, Color textBColor)
        {            
            text.color = textColor;
            background.color = textBColor;
        }
        public void Check()
        {
            if (notAnswered)
            {
                button.interactable = false;
                if (answer.isCorrect)
                {
                    correctEvents.Invoke();
                    StartCoroutine(ContinueCoroutine(continueWaitTime));
                }
                else
                    wrongEvents.Invoke();                
            }            
            if (answer.hasFeedback)
            {
                ShowFeedback();
            }
        }
        public void Free()
        {
            notAnswered = false;
            button.interactable = true;
        }
        public void ShowFeedback()
        {
            FeedbackController.Instance.Show("chat", answer.feedback, answer.isCorrect);
        }
        void Start()
        {
            button = GetComponent<Button>();
            notAnswered = true;
        }
        IEnumerator ContinueCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            question.CorrectAnswer();
        }
    }
}
