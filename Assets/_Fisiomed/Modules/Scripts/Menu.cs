using System;
using UnityEngine;

namespace Fisiomed.Main
{
    [Serializable]
    public class Menu
    {
        public Option[] options;
    }

    [Serializable]
    public class Option
    {
        public string url;
        public bool isActive;
        public string title;
        public string colorA;
        public string colorB;
        public string image;
    }
}
