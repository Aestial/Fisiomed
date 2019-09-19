using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Fisiomed.Questions_OLD
{
    public class QuizController : MonoBehaviour
    {
        [SerializeField] QuestionController questionController;
        [SerializeField] string url;

        Quiz quiz;
        int currentIndex = -1;
        const string filePath = "Quiz/quiz.xml";

        void Start()
        {
            questionController.quiz = this;
            StartCoroutine(Fetch(url));
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

        void Fetch()
        {
            var path = Path.Combine(Application.streamingAssetsPath, filePath);
            quiz = Quiz.Load(path);
        }

        IEnumerator Fetch(string url)
        {
            using (WWW www = new WWW(url))
            {
                yield return www;
                quiz = Quiz.LoadFromText(www.text);
                Debug.Log(www.text);
                Play();
            }
        }
    }
}
