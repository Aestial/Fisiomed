namespace Fisiomed.UI.Validator
{
    public interface IValidator
    {
        PageValidator Page
        {
            set;
        }
        bool IsValid
        {
            get;
        }
        void OnInvalid(string error);
        void OnValid();
    }
}
