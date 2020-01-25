using UnityEngine;
using TMPro;

public class PasswordUncover : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    bool isCovered = true;
    public void ToggleCover(bool isActive)
    {
        if (isCovered) Uncover();
        else Cover();

        inputField.ActivateInputField();
        inputField.DeactivateInputField();
    }
    private void Cover()
    {
        isCovered = true;
        inputField.contentType = TMP_InputField.ContentType.Password;
    }
    private void Uncover()
    {
        isCovered = false;
        inputField.contentType = TMP_InputField.ContentType.Standard;
    }    
}
