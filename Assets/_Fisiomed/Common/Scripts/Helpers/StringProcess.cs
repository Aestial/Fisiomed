public static class TextProcess
{
    public static string Process(string input)
    {
        string output;
        output = input.Replace("{username}", "Jaime");
        return output;
    }
}