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
        ChatController chat;
        //CanvasGroup answersGroup;
        // Question question;        
        #region Public Methods
        public void Set(ChatController chat, Question question, Character character, Sprite sprite)
        {
            this.chat = chat;
            SetAnswers(question, character);
            SetSprite(sprite);
        }
        //void EnableAnswers(bool isEnabled)
        //{
        //    answersGroup.interactable = isEnabled;
        //}
        public void CorrectAnswer()
        {
            Debug.Log("Correct answer!");
            //EnableAnswers(false);
            FreeAnswers();
            chat.NextBubble();
        }
        void SetAnswers(Question question, Character character)
        {
            // this.question = question;
            Print(question, character);
            //EnableAnswers(true);
        }
        void SetSprite(Sprite sprite)
        {
            characterImage.sprite = sprite;
        }
        void Print(Question question, Character character)
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
        private void FreeAnswers()
        {
            int length = container.childCount;
            for (int i = 0; i < length; i++)
            {
                AnswerController ac = container.GetChild(i).GetComponent<AnswerController>();
                ac.Free();
                //Transform answer = this.container.transform.GetChild(i);
                //Destroy(answer.gameObject);
            }
        }        
        // void SetColors(string text, string textB, string faceB)
        // {
        //     Color textColor = new Color();
        // 	Color textBColor = new Color();
        // 	Color faceBColor = new Color();
        // 	ColorUtility.TryParseHtmlString (text, out textColor);
        // 	ColorUtility.TryParseHtmlString (textB, out textBColor);
        // 	ColorUtility.TryParseHtmlString (faceB, out faceBColor);
        //     SetColors(textColor, textBColor, faceBColor);
        // }
        // void SetColors(Color textColor, Color textBColor, Color charBColor)
        // {            
        //     text.color = textColor;
        //     textBubble.color = textBColor;
        //     characterBubble.color = charBColor;            
        // }       
    }
}
