using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Fisiomed.Quiz
{
    public class AnswerController : MonoBehaviour 
    {
        public QuestionController question;
        public bool isCorrect;
        [SerializeField] Button button;
        [SerializeField] TMP_Text text;
        [SerializeField] float continueWaitTime;
        [SerializeField] UnityEvent correctEvents;
        [SerializeField] UnityEvent wrongEvents;

        public void Set(QuestionController question, Answer answer)
        {
            this.question = question;
            this.isCorrect = answer.isCorrect;
            this.text.text = answer.text;
        }        
        public void Check ()
        {
            if (isCorrect) {
                correctEvents.Invoke();
                question.EnableAnswers(false);
                StartCoroutine(ContinueCoroutine(continueWaitTime));
            } else {
                wrongEvents.Invoke();
            }
            button.interactable = false;
        }
        IEnumerator ContinueCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            question.CorrectAnswer();
        }
    }
}
