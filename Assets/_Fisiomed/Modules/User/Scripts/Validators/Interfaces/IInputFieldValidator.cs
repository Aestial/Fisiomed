namespace Fisiomed.UI.Validator
{
    public interface IInputFieldValidator : IValidator
    {
        void Validate(string content);
    }
}
