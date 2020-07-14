using System;

namespace Fisiomed.Case
{
    [Serializable]
    public class CCase
    {
        public Info info;
        public Chat.Chat chat;
        public Quiz.Quiz quiz;
    }
    [Serializable]
    public class Info
    {
        public string description;
        public string imageUrl;
    }
}