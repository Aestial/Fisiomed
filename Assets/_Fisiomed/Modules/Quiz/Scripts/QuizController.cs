using UnityEngine;
using Fisiomed.Feedback;
using System;

namespace Fisiomed.Quiz
{
    public class QuizController : Singleton<QuizController>, ILoader
    {
        [SerializeField] QuestionController questionController;        
        [SerializeField] int currentIndex = -1;
        [SerializeField] bool hasDefault;
        [SerializeField] string json;
        Quiz quiz;
        Notifier notifier = new Notifier();
        
        void Start()
        {
            CheckDefault(hasDefault, json);
            notifier.Subscribe(FeedbackController.ON_MODAL_CLOSED, HandleOnModalClosed);
        }
        void OnDestroy()
        {
            notifier.UnsubcribeAll();
        }
        private void HandleOnModalClosed(object[] args)
        {
            if((string)args[0] == "quiz")
                Play();
        }
        public void Set(Quiz quiz)
        {
            this.quiz = quiz;
            questionController.quiz = this;
            Play();
        }        
        public void Correct()
        {
            Play();
        }
        void Display(int index)
        {
            Question current = quiz.questions[index];
            questionController.Print(current);
        }
        void Play()
        {
            if (currentIndex < quiz.questions.Length - 1)
            {
                Display(++currentIndex);
            }
            else
            {
                Debug.Log("Finished Quiz!");
            }
        }
        public void CheckDefault(bool hasDefault, string json)
        {
            if (hasDefault)
            {
                Set(JsonUtility.FromJson<Quiz>(json));
            }
        }
    }
}
