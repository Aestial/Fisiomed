using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;

public class TestController : MonoBehaviour
{
    [SerializeField] private QuestionController questionController;
    private Test test;
    private int index = -1;
    private const string filePath = "Resources/Tests/test.xml";

    void Start()
    {
        this.questionController.test = this;
        this.Fetch();
        this.Play();
    }
    
    public void Correct()
    {
        this.Play();
    }

    private void Display(int index)
    {
        Question current = this.test.questions[index];
        this.questionController.Print(current);
    }

    private void Play()
    {
        if (this.index < this.test.questions.Length - 1) {
            this.Display(++this.index);
        } else {
            Debug.Log("Finished Test!");
        }
    }

    private void Fetch()
    {
        var path = Path.Combine(Application.dataPath, filePath);
        this.test = Test.Load(path);
    }

}