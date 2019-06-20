using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fisiomed.Chat
{
    [Serializable]
    public class Character
    {
        public string name;
        public string side;
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

    [Serializable]
    public class Chat
    {
        public Character[] characters;
        public Dialog[] dialogs;
    }
}
