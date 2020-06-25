﻿using Fisiomed.User;

public static class TextProcess
{
    public static string Process(string input)
    {
        string output;
        // Define gender
        string gender = UserManager.Instance.User.personal.gender;
        gender = gender.Replace("Male", "el").Replace("Female", "la");
        // Replace name
        output = input.Replace("{username}", UserManager.Instance.User.personal.name);
        // Replace gender
        output = output.Replace("{gender}", gender);
        return output;
    }
}