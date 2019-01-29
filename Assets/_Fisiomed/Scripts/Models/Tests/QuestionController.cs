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
    [SerializeField] private GameObject feedbackPanel;

    CanvasGroup answersGroup;
    
    private Question question;
    private const string filePath = "Resources/Quiz/question.xml";

    public QuizController quiz;

    void Awake()
    {
        answersGroup = answersPanel.GetComponent<CanvasGroup>();
        Debug.Log(answersGroup);
    }

    public void EnableAnswers(bool enabled)
    {
        answersGroup.interactable = enabled;
    }

    public void CorrectAnswer()
    {
        Debug.Log("Correct answer!");
        if(this.question.hasFeedback) {
            this.ShowFeedback();
        }
        else {
            this.quiz.Correct();
        }
    }

    private void ShowFeedback()
    {
        feedbackPanel.SetActive(true);
    }

    public void Print(Question question)
    {
        this.question = question;
        ClearAnswers();
        EnableAnswers(true);
        Print();
    }

    public void Print()
    {
        questionText.text = this.question.text;        
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
#if UNITY_ANDROID
        path = "jar:file://" + path;
#endif
        this.question = Question.Load(path);
    }

}