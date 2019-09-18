using System;

namespace Fisiomed.Chat
{
    [Serializable]
    public class Chat
    {
        public Element[] dialogue;
        public Character[] characters;
        public Message[] messages;
        public Question[] questions;
    }    
    [Serializable]
    public class Element
    {
        public ElementType type;
        public int character;
        public int message;
        public int question;
    }
    public enum ElementType
    {
        Message,
        Question
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
    public class Question
    {
        public string text;
        public bool showFeedback;
        public Answer[] answers;
    }
    [Serializable]
    public class Answer
    {
        public string text;
        public bool isCorrect;
        public string feedback;
    }
    
}
