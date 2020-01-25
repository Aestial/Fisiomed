﻿using UnityEngine;
using TMPro;

public class PasswordValidator : MonoBehaviour, IInputValidator
{
    [SerializeField] internal TMP_Text message;
    [SerializeField] internal int minCharacters = 8;
    [SerializeField] internal string errorMessage;
    [SerializeField] internal string emptyErrorMessage;
    private void Start()
    {
        message.enabled = false;
    }
    public void Validate(string content)
    {
        if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
        {
            OnInvalid(emptyErrorMessage);
            return;
        }
        if(content.Length < minCharacters)
        {
            OnInvalid(errorMessage);
            return;
        }
        OnValid();
    }
    public void OnValid()
    {
        message.enabled = false;
    }
    public void OnInvalid(string error)
    {
        message.enabled = true;
        message.text = error;        
    }    
}
