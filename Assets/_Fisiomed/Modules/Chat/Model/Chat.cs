using System;

namespace Fisiomed.Chat
{
    [Serializable]
    public class Chat
    {
        public Character[] characters;
        public Dialog[] dialogs;
    }

    [Serializable]
    public class Character
    {
        public string name;
        public string side;
        public string textColor;
        public string faceBColor;
        public string textBColor;
        public string imageUrl;
    }

    [Serializable]
    public class Dialog
    {
        public int character;
        public string message;
    }
}
