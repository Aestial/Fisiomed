namespace Fisiomed.UI.Validator
{
    public class MailValidateFunction : ValidateFunction
    {
        override public bool IsValid(string content)
        {
            return RegexUtilities.IsValidEmail(content);
        }
    }
}
