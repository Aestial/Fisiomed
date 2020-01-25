using System;

namespace Fisiomed.User
{
    [Serializable]
    public class User
    {
        public string id; // DB ID
        public string email; // Unique identifier
        public string password;
        public GameProfile gamer;
        public PersonalProfile personal;
    }
    [Serializable]
    public class GameProfile
    {
        // Game information
        public string username;
        public string avatar;
        // TODO: PROGRESS
        public int level;
    }    
    [Serializable]
    public class PersonalProfile
    {
        // Real name
        public string name;
        public string surname;
        // User personal info
        public string gender;
        public string age;
        public string carreer;
        public string university;
        public string semester;
        public string unamID;
    }
}
