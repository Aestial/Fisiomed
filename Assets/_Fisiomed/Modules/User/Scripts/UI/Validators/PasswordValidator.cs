using UnityEngine;
using TMPro;

public class PasswordValidator : MonoBehaviour, IInputValidator
{
    [SerializeField] private TMP_Text message;
    [SerializeField] private int minCharacters = 8;
    [SerializeField] private string errorMessage;
    private void Start()
    {
        message.enabled = false;
    }
    public void Validate(string content)
    {
        bool invalid = string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content);
        invalid |= content.Length < minCharacters;
        if (invalid)
            OnInvalid(errorMessage);
        else
            OnValid();
    }
    public void OnInvalid(string error)
    {
        message.enabled = true;
        message.text = error;        
    }
    public void OnValid()
    {
        message.enabled = false;
    }
}
