using System;

namespace Fisiomed.Chat
{
    [Serializable]
    public class Chat
    {
        public Element[] sequence;
        public Character[] characters;
        public Message[] messages;
        public Interactive[] interactives;
        public Question[] questions;
    }    
    [Serializable]
    public class Element
    {
        public int type;
        public int index;
        public int character;
    }
    [Serializable]
    public enum ElementType
    {
        Message,
        Question,
        Interactive,
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
    public class Message
    {
        public string text;
    }
    [Serializable]
    public class Interactive
    {
        public string text;
        public string and;
        public string ios;
    }
    [Serializable]
    public class Question
    {
        public Answer[] answers;
    }
    [Serializable]
    public class Answer
    {
        public string text;
        public bool isCorrect;
        public bool hasFeedback;
        public string feedback;
    }
}
