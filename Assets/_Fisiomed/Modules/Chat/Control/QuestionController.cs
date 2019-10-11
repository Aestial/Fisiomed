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
        CanvasGroup answersGroup;
        // Question question;        
        #region Public Methods
        public void Set(ChatController chat, Question question, Character character, Sprite sprite)
        {
            this.chat = chat;
            SetAnswers(question, character);
        }
        void EnableAnswers(bool enabled)
        {
            answersGroup.interactable = enabled;
        }
        public void CorrectAnswer()
        {
            Debug.Log("Correct answer!");
            EnableAnswers(false);
            chat.NextBubble();
        }
        void SetAnswers(Question question, Character character)
        {
            // this.question = question;
            Print(question, character);
            EnableAnswers(true);
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
        private void ClearAnswers()
        {
            int length = this.container.transform.childCount;
            for (int i = 0; i < length; i++)
            {
                Transform answer = this.container.transform.GetChild(i);
                Destroy(answer.gameObject);
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
        void Awake()
        {
            answersGroup = container.GetComponent<CanvasGroup>();
            Debug.Log(answersGroup);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
