namespace Fisiomed.UI.Validator
{
    public interface IValidator
    {
        void Validate();
        void Subscribe(Validatable validatable);
        void Unsubscribe(Validatable validatable);
    }
}
