namespace Fisiomed.UI.Validator
{
    public interface IValidatable
    {
        void OnValid();
        void OnInvalid(string error);
        IValidator Validator
        {
            set;
        }
        bool IsValid
        {
            get;
        }        
    }
}
