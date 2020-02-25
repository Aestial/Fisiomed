using Fisiomed.User;

public static class TextProcess
{
    public static string Process(string input)
    {
        string output;
        output = input.Replace("{username}", UserManager.Instance.User.gamer.username);
        return output;
    }
}