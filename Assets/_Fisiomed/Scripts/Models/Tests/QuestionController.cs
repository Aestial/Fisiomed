using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;
using TMPro;

public class QuestionController : MonoBehaviour
{
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private Transform answersPanel;
    [SerializeField] private GameObject answerPrefab;
    
    private Question question;
    private const string filePath = "Resources/Tests/question.xml";

    public TestController test;
   
    public void CorrectAnswer()
    {
        Debug.Log("Correct answer!");
        this.test.Correct();
    }

    public void Print(Question question)
    {
        this.question = question;
        this.Print();
    }

    public void Print()
    {
        this.ClearAnswers();
        this.questionText.text = this.question.text;        
        int length = this.question.answers.Length;
        for (int i = 0; i < length; i++ )
        {
            Answer answer = this.question.answers[i];
            GameObject answerGO = Instantiate(answerPrefab, answersPanel);
            AnswerController ac = answerGO.GetComponent<AnswerController>();
            ac.Set(this, answer);
        } 
    }

    private void ClearAnswers()
    {
        int length = this.answersPanel.transform.childCount;
        for(int i = 0; i < length; i++)
        {
            Transform answer = this.answersPanel.transform.GetChild(i);
            Destroy(answer.gameObject);
        }
    }
    
    private void Fetch()
    {
        var path = Path.Combine(Application.dataPath, filePath);
        this.question = Question.Load(path);
    }

}