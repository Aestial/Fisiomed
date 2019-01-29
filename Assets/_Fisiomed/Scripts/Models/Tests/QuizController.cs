using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    [SerializeField] QuestionController questionController;

    Quiz test;
    int currentIndex = -1;
    const string filePath = "Quiz/quiz.xml";

    void Start()
    {
        questionController.quiz = this;
        Fetch();
        Play();
    }
    
    public void Correct()
    {
        Play();
    }

    void Display(int index)
    {
        Question current = test.questions[index];
        questionController.Print(current);
    }

    void Play()
    {
        if (currentIndex < test.questions.Length - 1)
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
        test = Quiz.Load(path);
    }

}