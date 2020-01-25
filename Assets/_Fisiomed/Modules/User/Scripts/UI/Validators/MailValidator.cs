using UnityEngine;
using TMPro;

public class MailValidator : MonoBehaviour, IInputValidator
{
    [SerializeField] private TMP_Text message;
    [SerializeField] private string errorMessage;
    [SerializeField] private string emptyErrorMessage;
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
        if (!RegexUtilities.IsValidEmail(content))
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
