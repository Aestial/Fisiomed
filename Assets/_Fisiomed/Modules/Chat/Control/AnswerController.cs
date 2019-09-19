using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

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
        FeedbackController feedback;
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
            if (answer.hasFeedback)
            {
                ShowFeedback();
            }
            if (answer.isCorrect) {
                correctEvents.Invoke();                           
                StartCoroutine(ContinueCoroutine(continueWaitTime));
            } else {
                wrongEvents.Invoke();
            }
            button.interactable = false;
        }

        public void ShowFeedback()
        {
            feedback.Show(answer.feedback, answer.isCorrect);
        }

        void Start()
        {
            feedback = FindObjectOfType<FeedbackController>();
            button = GetComponent<Button>();
        }

        IEnumerator ContinueCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            question.CorrectAnswer();
        }
    }
}
