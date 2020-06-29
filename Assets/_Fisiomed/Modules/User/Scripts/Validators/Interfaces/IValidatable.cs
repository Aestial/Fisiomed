namespace Fisiomed.UI.Validator
{
    public interface IValidatable
    {
        void SetValid(string content);
        void SetInvalid(string error);
        bool IsValid
        {
            get;
        }        
    }
}
