using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField] QuestionController questionController;

    Test test;
    int currentIndex = -1;
    const string filePath = "Resources/Tests/test.xml";

    void Start()
    {
        questionController.test = this;
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
            Debug.Log("Finished Test!");
        }
    }

    void Fetch()
    {
        var path = Path.Combine(Application.dataPath, filePath);
        test = Test.Load(path);
    }

}