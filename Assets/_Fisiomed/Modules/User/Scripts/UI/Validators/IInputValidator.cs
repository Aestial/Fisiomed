public interface IInputValidator
{
    void Validate(string content);
    void OnInvalid(string error);
    void OnValid();
}
