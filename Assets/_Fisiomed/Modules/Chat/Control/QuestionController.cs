using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed.Chat
{
    public class QuestionController : MonoBehaviour
    {
        [Header("Customize Components")]
        [SerializeField] Image characterImage;
        [SerializeField] Image characterBubble;
        [SerializeField] GameObject answerBubblePrefab;
        [SerializeField] private Transform container;
        ChatManager chat;
        CanvasGroup answersGroup;
        // Question question;        
        #region Public Methods
        public void Set(ChatManager chat, Question question, Character character, Sprite sprite)
        {
            this.chat = chat;
            SetAnswers(question, character);
        }
        public void CorrectAnswer()
        {
            Debug.Log("Correct answer!");
            EnableAnswers(false);
            chat.NextBubble();
        }        
        #endregion
        #region Private Methods
        private void ClearAnswers()
        {
            int length = this.container.transform.childCount;
            for (int i = 0; i < length; i++)
            {
                Transform answer = this.container.transform.GetChild(i);
                Destroy(answer.gameObject);
            }
        }
        private void EnableAnswers(bool enabled)
        {
            answersGroup.interactable = enabled;
        }
        private void SetAnswers(Question question, Character character)
        {
            // this.question = question;
            Print(question, character);
            EnableAnswers(true);
        }
        private void Print(Question question, Character character)
        {
            // questionText.text = this.question.text;        
            int length = question.answers.Length;
            for (int i = 0; i < length; i++)
            {
                GameObject newGO = Instantiate(answerBubblePrefab, container);
                AnswerController answer = newGO.GetComponent<AnswerController>();
                answer.Set(this, question.answers[i], character);
            }
        }
        #endregion
        #region MonoBehaviour Methods
        void Start()
        {
            answersGroup = container.GetComponent<CanvasGroup>();
            Debug.Log(answersGroup);
        }
        #endregion
    }
}
