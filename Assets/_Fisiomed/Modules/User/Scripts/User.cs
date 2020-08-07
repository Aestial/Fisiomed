using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;

namespace Fisiomed.User
{
    [Serializable]
    public class StringStringDictionary : SerializableDictionaryBase<string,string> { }
    [Serializable]
    public class IntStringDictionary : SerializableDictionaryBase<int,string> { }

    [Serializable]
    public class UserData
    {
        public string id; // DB ID
        public StringStringDictionary properties;
        public IntStringDictionary progress;
        public UserData()
        {
            properties = new StringStringDictionary();
            progress = new IntStringDictionary();
        }
    }
    //[Serializable]
    // propierties:
    //    // Real name
    //    public string name;
    //    public string f-surname;
    //    public string m-surname;
    //    // User personal info
    //    public string gender;
    //    public string age;
    //    public string career;
    //    public string university;
    //    public string semester;
    //    public string unamID;
    //    // Game information
    //    public string username;
    //    public string avatar;
    //    public int level;
}
