using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;

namespace Fisiomed.User
{
    [Serializable]
    public class StringStringDictionary : SerializableDictionaryBase<string,string> { }
    
    [Serializable]
    public class UserData
    {
        public string id; // DB ID
        public StringStringDictionary properties;
        //public GameProfile gamer;
        //public PersonalProfile personal;
        public UserData()
        {
            //gamer = new GameProfile();
            //personal = new PersonalProfile();
            properties = new StringStringDictionary();
        }
    }
    [Serializable]
    public class GameProfile
    {
        // Game information
        public string username;
        public string avatar;
        public int level;
    }    
    //[Serializable]
    //public class PersonalProfile
    //{
    //    // Real name
    //    public string name;
    //    public string surname;
    //    // User personal info
    //    public string gender;
    //    public string age;
    //    public string career;
    //    public string university;
    //    public string semester;
    //    public string unamID;
    //}
}
