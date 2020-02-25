namespace Fisiomed.UI.Validator
{
    public abstract class InputField : Validatable, IInputField
    {                
        public virtual void Validate(string content)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))            
                OnInvalid(errorMessage);
            else             
                OnValid();
            Validator.Validate();
        }        
    }
}