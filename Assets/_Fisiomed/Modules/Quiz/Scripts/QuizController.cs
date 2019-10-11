using UnityEngine;

namespace Fisiomed.Quiz
{
    public class QuizController : Singleton<QuizController>
    {
        [SerializeField] QuestionController questionController;
        Quiz quiz;
        int currentIndex = -1;

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
    }
}
