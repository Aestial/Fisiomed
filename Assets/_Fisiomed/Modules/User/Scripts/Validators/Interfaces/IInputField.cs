namespace Fisiomed.UI.Validator
{
    public interface IInputField : IValidatable
    {
        void Validate(string content);
    }
}
