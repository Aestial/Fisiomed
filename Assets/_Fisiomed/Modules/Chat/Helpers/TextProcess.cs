using System.Collections.Generic;
using UnityEngine;
using Fisiomed.User;

public static class TextProcess
{
    public static string Process(string input)
    {
        string output;

        // Define gender
        string gender;
        try
        {
            gender = UserManager.Instance.UserDataAux.properties["gender"];
        }
        catch (KeyNotFoundException e)
        {
            Debug.Log("Property Gender: " + e.Message);
            gender = "Female";
        }            
        gender = gender.Replace("Male", "el").Replace("Female", "la");

        // Define name
        string name;
        try
        {
            name = UserManager.Instance.UserDataAux.properties["name"];
        }
        catch (KeyNotFoundException e)
        {
            Debug.Log("Property Name: " + e.Message);
            name = "Amig@";
        }
        // Replace name
        output = input.Replace("{username}", name);
        // Replace gender
        output = output.Replace("{gender}", gender);

        return output;
    }
}